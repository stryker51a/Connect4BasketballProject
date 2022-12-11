using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NewGameLobby : MonoBehaviour
{

    public string newGameScene;
    public string mainMenuScene;
    public TextMeshProUGUI room1Players;
    public TextMeshProUGUI room2Players;
    public TextMeshProUGUI room3Players;
    public TextMeshProUGUI room4Players;
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject room4;
    public GameObject room1Button;
    public GameObject room2Button;
    public GameObject room3Button;
    public GameObject room4Button;
    public GameObject leaveRoomButton;



    private string lobbyCodeInput;
    private Connect4Manager connect4Manager;

    // Start is called before the first frame update
    void Start()
    {
        connect4Manager = Connect4Manager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayRoom1Players();
        DecideColor(room1, room1Players);
        DisplayRoom2Players();
        DecideColor(room2, room2Players);
        DisplayRoom3Players();
        DecideColor(room3, room3Players);
        DisplayRoom4Players();
        DecideColor(room4, room4Players);

        setButtonColors(connect4Manager.cur_room_name());

        if (connect4Manager.cur_room_name() == ""){
            leaveRoomButton.SetActive(false); 
        } else {
            leaveRoomButton.SetActive(true);
        }


        
    }

    public void Go()
    {
        Debug.Log("Called Go");
        connect4Manager.ButtonJoinRoom(lobbyCodeInput);
        //SceneManager.LoadScene(newGameScene);


    }

    public void Back()
    {

        GameObject g1 = connect4Manager.Back();
        Object.Destroy(g1);
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ReadLobbyCode(string code){
        lobbyCodeInput = code;
    }

    public void DisplayRoom1Players()
    {
        room1Players.text = connect4Manager.getRoomPlayers("test 1");
    }

    public void DisplayRoom2Players()
    {
        room2Players.text = connect4Manager.getRoomPlayers("test 2");
    }

    public void DisplayRoom3Players()
    {
        room3Players.text = connect4Manager.getRoomPlayers("test 3");
    }

    public void DisplayRoom4Players()
    {
        room4Players.text = connect4Manager.getRoomPlayers("test 4");
    }

    public void DecideColor(GameObject room, TextMeshProUGUI roomPlayers)
    {

        if (roomPlayers.text == "2")
        {
            room.GetComponent<Image>().color = Color.red;
        }
        else if (roomPlayers.text == "1")
        {
            room.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            room.GetComponent<Image>().color = Color.green;
        }
    }

    public void setButtonColors(string cur_room){
        string[] room_names = {"test 1", "test 2", "test 3", "test 4"};
        GameObject[] buttons = {room1Button, room2Button, room3Button, room4Button};

        for (int i = 0; i < room_names.Length; i++){
            if (cur_room == room_names[i]){
                buttons[i].GetComponent<Image>().color = Color.green;
            } else {
                buttons[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void JoinRoom1()
    {
        connect4Manager.ButtonJoinRoom("test 1");
    }

    public void JoinRoom2()
    {
        connect4Manager.ButtonJoinRoom("test 2");
    }

    public void JoinRoom3()
    {
        connect4Manager.ButtonJoinRoom("test 3");
    }

    public void JoinRoom4()
    {
        connect4Manager.ButtonJoinRoom("test 4");
    }

    public void ButtonLeaveRoom()
    {
        connect4Manager.ButtonLeaveRoom();
    }

    public void BeginGame()
    {
        connect4Manager.ButtonStartGame();
    }



}
