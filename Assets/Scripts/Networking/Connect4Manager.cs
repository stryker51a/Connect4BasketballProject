using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Connect4Manager : MonoBehaviourPunCallbacks
{

    public static Connect4Manager Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private NetworkManager networkManager;
    private bool gameRunning;
    struct team {
        public int id;
        public string color;

        public team(int _id, string _color){
            id = _id;
            color = _color;
        }
    }

    struct player_info {
        public int actor_id;
        public team team;

    }
    
    // Maps actor Number to team
    private List<int> player_ids;
    private Dictionary<int, player_info> player_teams;
    
    private List<team> team_list;

    private string current_turn;

    private team local_team;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = NetworkManager.Instance;
        DontDestroyOnLoad(GameObject.Find("Game Manager"));
        gameRunning = false;
        player_ids = new List<int>();
        player_teams = new Dictionary<int, player_info>();
        team_list = new List<team>();
        team_list.Add(new team(1, "red"));
        team_list.Add(new team(2, "yellow"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void StartGame(){
        if (gameRunning){
            return;
        }

        if (PhotonNetwork.IsMasterClient){
            //PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log("Loading Level...");
            PhotonNetwork.LoadLevel(2);
        }

        Dictionary<int, Player> playerList = networkManager.getPlayerList();

        Player local = PhotonNetwork.LocalPlayer;

        //player_ids.Add(local.ActorNumber);

        foreach (var p_val in playerList.Keys)
        {
            Player p1 = playerList[p_val];
            player_ids.Add(p1.ActorNumber);
        }

        player_ids.Sort();

        for (int i = 0; i < player_ids.Count; i++)
        {
            team t1;
            t1.id = team_list[i].id;
            t1.color = team_list[i].color;

            player_info p1;
            p1.actor_id = player_ids[i];
            p1.team = t1;

            player_teams[p1.actor_id] = p1;

            Debug.Log(p1.actor_id);
            Debug.Log(p1.team.color);
        }

        current_turn = team_list[0].color;

        local_team = player_teams[local.ActorNumber].team;

        gameRunning = true;

    }

    public void set_game_running(bool status){
        gameRunning = status;
    }
    

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
        gameRunning = false;
        if (PhotonNetwork.IsMasterClient){
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
        player_ids.Clear();
        player_teams.Clear();
		base.OnPlayerLeftRoom(otherPlayer);
	}

    public bool isGameRunning(){
        return gameRunning;
    }

        /// <summary>
        /// Get the List of player Ids in the current room if the game is starting
        /// </summary>
        /// <remarks>
        /// If the game has not started, returns an empty list
        /// </remarks>
    public List<int> getPlayerIds(){
        return player_ids;
    }

    /// <summary>
    /// Get the Team Id of a certain player from their player id
    /// </summary>
    /// <remarks>
    /// If the game has not started, returns -1
    /// </remarks>
    public int getPlayerTeamId(int actor_id){
        if (gameRunning){
            return player_teams[actor_id].team.id;
        } 
        
        return -1;
    }
    
    /// <summary>
    /// Get the color of the team of a certain player from their player id
    /// </summary>
    /// <remarks>
    /// If the game has not started, returns -1
    /// </remarks>
    public string getPlayerTeamColor(int actor_id){
        if (gameRunning){
            return player_teams[actor_id].team.color;
        }

        return "";
    }

    [PunRPC]
    public void test_rpc(){
        Debug.Log("testing RPC success");
    }

    public void ButtonJoinRoom(string name){
        networkManager.ButtonJoinRoom(name);
    }

    public void ButtonLeaveRoom(){
        networkManager.ButtonLeaveRoom();
    }

    
    public bool ButtonStartGame(){
        if (networkManager.readyToStart()){
            StartGame();
            return true;
        }
        return false;
    }


    public void Disconnect(){
        networkManager.Disconnect();
    }

    public bool IsConnected(){
        return networkManager.IsConnected();
    }

    public void reconnect(){
        if (!IsConnected()){
            networkManager.ConnectToServer();
        }
    }
    public void end_turn_network(string color){
        if (color == null){
            current_turn = current_turn.ToLower() == "red" ? "yellow" : "red";
        }
        else if (color.ToLower() == "red"){
            current_turn = "yellow";
        } else {
            current_turn = "red";
        }

        if (isMyTurn()){
            // enable xr Interactor
            set_hand_interactors(true);

        } else {
            // disable xr Interactor
            set_hand_interactors(false);
        }

        // enable or disable XR stuff

    }

    public string get_current_turn(){
        return current_turn;
    }

    public string end_turn(string current_color = null){
        end_turn_network(current_color);

        return current_turn;
    }


    // May be able to make private
    public bool isMyTurn(){
        return local_team.color == current_turn;
    }

    // Will set the hand interactors active field to status.
    public void set_hand_interactors(bool status){
        GameObject XROrigin = GameObject.Find("XR Origin");
        GameObject leftHand = GameObject.Find("XR Origin/Camera Offset/LeftHand Controller");
        GameObject rightHand = GameObject.Find("XR Origin/Camera Offset/RightHand Controller");

        SphereCollider sphereColliderRight = rightHand.GetComponent<SphereCollider>();
        
        SphereCollider sphereColliderLeft = leftHand.GetComponent<SphereCollider>();
        
        if (sphereColliderRight != null){
            sphereColliderRight.enabled = status;
        } else {
            Debug.Log("Sphere Collider Right is null");
        }

        if (sphereColliderLeft != null){
            sphereColliderLeft.enabled = status;
        } else {
            Debug.Log("Sphere Collider Left is null");
        }
    }

    public string getRoomPlayers(string name){
        return networkManager.getRoomPlayers(name);
    }

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("On Player entered room");
        ButtonStartGame();
	}

    public string get_local_team_color(){
        return local_team.color;
    }

	public override void OnJoinedRoom()
	{
        Debug.Log("On Joined room");
		base.OnJoinedRoom();

        // SWITCH THE COMMENT ON THESE LINES FOR TESTING

        //StartGame();
        ButtonStartGame();
	}

    public string cur_room_name(){
        if (PhotonNetwork.InRoom){
            return PhotonNetwork.CurrentRoom.Name;
        }

        return "";
    }

    public GameObject Back(){
        if (PhotonNetwork.InRoom){
            PhotonNetwork.LeaveRoom();
        }
        PhotonNetwork.Disconnect();
        GameObject manager = GameObject.Find("Game Manager");
        GameObject network = GameObject.Find("Network Manager");

        Object.Destroy(network);

        return manager;

    }

}
