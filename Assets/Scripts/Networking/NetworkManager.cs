using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    private const int PLAYERS = 2;
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    // Start is called before the first frame update

    //Connect4Manager connect4Manager;

    public static NetworkManager Instance { get; private set; }
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

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Network Manager"));
        //connect4Manager = Connect4Manager.Instance;
        ConnectToServer();
    }

    // Update is called once per frame
    public void ConnectToServer() {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    public override void OnConnectedToMaster(){
        Debug.Log("Connected to Server");
        base.OnConnectedToMaster();
        // RoomOptions roomOptions = new RoomOptions();
        // roomOptions.MaxPlayers = 10;
        // roomOptions.IsVisible = true;
        // roomOptions.IsOpen = true;
        // roomOptions.PublishUserId = true;

        //Debug.Log("Attempting to join lobby...");
        PhotonNetwork.JoinLobby();
        //PhotonNetwork.JoinOrCreateRoom("Room 2", roomOptions, TypedLobby.Default);


    }

    public void ButtonJoinRoom(string RoomName){
        if (!PhotonNetwork.InLobby){
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = PLAYERS;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.PublishUserId = true;

        Debug.Log("Joining room: " + RoomName);
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
    }

    public void ButtonLeaveRoom(){
        if (PhotonNetwork.InLobby){
            return;
        }

        if (PhotonNetwork.InRoom){
            PhotonNetwork.LeaveRoom();
        }
        PhotonNetwork.JoinLobby();
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for(int i=0; i<roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }

	// public override void OnJoinedRoom()
	// {
    //     Debug.Log("Joined a room");

    //     Room r1 = PhotonNetwork.CurrentRoom;

    //     //connect4Manager.ButtonStartGame();
        

    //     // if (PhotonNetwork.IsMasterClient){
    //     //     Debug.Log("Loading Level...");
    //     //     PhotonNetwork.LoadLevel(0);
    //     // }

	// 	base.OnJoinedRoom();
	// }

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
        Debug.Log("New Player Joined a Room");

		base.OnPlayerEnteredRoom(newPlayer);
	}

	public override void OnJoinedLobby()
	{
        Debug.Log("Joined a Lobby");
        cachedRoomList.Clear();

        PhotonNetwork.LoadLevel(1);

		base.OnJoinedLobby();
	}

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
        base.OnRoomListUpdate(roomList);
    }

    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        cachedRoomList.Clear();
    }

    public Dictionary<int, Player> getPlayerList(){
        return PhotonNetwork.CurrentRoom.Players;
    }

    public bool readyToStart(){
        if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == PLAYERS){
            return true;
        }
        return false;
    }

    public void Disconnect(){
        PhotonNetwork.Disconnect();
    }

    public bool IsConnected(){
        return PhotonNetwork.IsConnected;
    }

    public string getRoomPlayers(string name){
        if (!cachedRoomList.ContainsKey(name)){
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.Name == name){
                return "1";
            }
            return "0";
        }
        return cachedRoomList[name].PlayerCount.ToString();
    }
}
