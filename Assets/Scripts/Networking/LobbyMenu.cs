using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class LobbyMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private Connect4Manager connect4Manager;

    void Start()
    {
        connect4Manager = Connect4Manager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinRoom1(){
        connect4Manager.ButtonJoinRoom("test 1");
    }

    public void JoinRoom2(){
        connect4Manager.ButtonJoinRoom("test 2");
    }

    public void JoinRoom3(){
        connect4Manager.ButtonJoinRoom("test 3");
    }

    public void JoinRoom4(){
        connect4Manager.ButtonJoinRoom("test 4");
    }

    public void ButtonLeaveRoom(){
        connect4Manager.ButtonLeaveRoom();
    }

    public void BeginGame(){
        connect4Manager.ButtonStartGame();
    }
}
