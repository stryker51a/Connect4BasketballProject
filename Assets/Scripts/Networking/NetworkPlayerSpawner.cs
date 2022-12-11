using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;

	private Connect4Manager connect4Manager;

    private bool spawned_player;

	public void spawner_start_game_red(){
		spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
	}

	public void spawner_start_game(){

        GameObject XROrigin = GameObject.Find("XR Origin");

        Debug.Log("Team color: " + connect4Manager.get_local_team_color());

		if (PhotonNetwork.InRoom && connect4Manager.get_local_team_color() == "yellow"){
            XROrigin.transform.position += Vector3.forward * 3.0f;
		}

		spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);

	}

	public void spawner_start_game_yellow(){
		spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
	}

	void Start() {
		Debug.Log("Spawner Start");
        
        spawned_player = false;
		connect4Manager = Connect4Manager.Instance;

		// if (connect4Manager.isGameRunning() && spawnedPlayerPrefab == null){
		// 	Debug.Log("spawner start game");
		// 	spawner_start_game();
		// }
		
	}

// Update is called once per frame
	void Update()
	{
		if (PhotonNetwork.IsConnected && connect4Manager.isGameRunning() && !spawned_player){
			Debug.Log("spawner start game");
            spawned_player = true;
			spawner_start_game();
		}
	}

	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		Debug.Log("Spawner On Join Room");
		//spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
	}

	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		PhotonNetwork.Destroy(spawnedPlayerPrefab);
	}

}

/**
public class Network_Player_Controller : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private BaseData BD;
    private int avatar_choice;
    [HideInInspector]
    public GameObject Environment;
    [HideInInspector]
    public List<float> xPos = new List<float>();
    private GameObject XROrigin, Canvas, QuestionText;
    [HideInInspector]
    public List<GameObject> ObjectList = new List<GameObject>();
    [HideInInspector]
    public int PlayerCount = 0;
    [HideInInspector]
    public bool updateStopper;
    private int i = 0;

    private void Start()
    {
        XROrigin = GameObject.Find("XR Origin");
        Canvas = GameObject.Find("Canvas");
        QuestionText = GameObject.Find("Canvas/Question Text");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (BD.Gender == 1)
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Male Player", transform.position, transform.rotation);
            this.GetComponent<PhotonView>().RPC("PlayerCounter", RpcTarget.AllBuffered);
        }
        else if (BD.Gender == 2)
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Female Player", transform.position, transform.rotation);
            this.GetComponent<PhotonView>().RPC("PlayerCounter", RpcTarget.AllBuffered);
        }
        else
        {
            int[] avatar_choices = { 1, 2 };
            int NoOfChoices = avatar_choices.Length;
            int ChoiceChosen = Random.Range(0, NoOfChoices);
            avatar_choice = avatar_choices[ChoiceChosen];

            if (avatar_choice == 1)
            {
                spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Male Player", transform.position, transform.rotation);
                this.GetComponent<PhotonView>().RPC("PlayerCounter", RpcTarget.AllBuffered);
            }
            else
            {
                spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Female Player", transform.position, transform.rotation);
                this.GetComponent<PhotonView>().RPC("PlayerCounter", RpcTarget.AllBuffered);
            }
        }

        Environment.transform.position += new Vector3(0, 0, 1.7f);
        //xPos = Random.Range(-2.0f, 2.0f);
        //XROrigin.transform.position += new Vector3(xPos, 0, 0);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            if (Canvas.activeSelf == false)
                Canvas.SetActive(true);

            if (QuestionText.activeSelf == false)
                QuestionText.SetActive(true);

            QuestionText.GetComponent<TMP_Text>().text = "Explore the maze for 10 minutes\nand remember the locations of the objects";
        }

        if (PlayerCount == 2 && PhotonNetwork.IsMasterClient && updateStopper == false)
        {
            xPos.Add(-0.5f);
            xPos.Add(0.5f);
            Player_Arranger(i);
        }
        else if (PlayerCount == 3 && PhotonNetwork.IsMasterClient && updateStopper == false)
        {
            xPos.Add(-1.0f);
            xPos.Add(0.0f);
            xPos.Add(1.0f);
            Player_Arranger(i);
        }
    }

    private void Player_Arranger(int i)
    {
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("Network_Player"))
        {
            if (element.GetComponent<PhotonView>().IsMine)
                element.GetComponent<PhotonView>().RPC("InitialCalibration", RpcTarget.AllBuffered, xPos[i], element, i);
            else
                element.GetComponent<PhotonView>().RPC("OtherInitialCalibration", RpcTarget.AllBuffered, xPos[i], element, i);

            i += 1;
        }

        updateStopper = true;
    }

    [PunRPC]
    public void InitialCalibration(float xPosition, GameObject element, int ObjectListLocation)
    {
        XROrigin.transform.position += new Vector3(xPosition, 0, 0);
        ObjectList[ObjectListLocation] = element;
    }

    [PunRPC]
    public void OtherInitialCalibration(float xPosition, GameObject element, int ObjectListLocation)
    {
        if (element.name == "Network Male Player(Clone)")
            element.GetComponent<Network_Male_Player>().XROrigin.transform.position += new Vector3(xPosition, 0, 0);
        else if (element.name == "Network Female Player(Clone)")
            element.GetComponent<Network_Female_Player>().XROrigin.transform.position += new Vector3(xPosition, 0, 0);

        ObjectList[ObjectListLocation] = element;
    }

    [PunRPC]
    public void PlayerCounter()
    {
        PlayerCount += 1;
    }
}
*/