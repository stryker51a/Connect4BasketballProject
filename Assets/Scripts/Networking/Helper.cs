using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Helper : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    Connect4Manager connect4Manager;

    void Start()
    {
        connect4Manager = Connect4Manager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string end_turn(string current_turn_color = null){
        Debug.Log("calling end turn");
        photonView.RPC("net_end_turn", RpcTarget.AllBuffered, current_turn_color);
        Debug.Log("current turn: " + connect4Manager.get_current_turn());
        return connect4Manager.get_current_turn();
    }

    [PunRPC]
    public void net_end_turn(string color){
        connect4Manager.end_turn(color);
    }




}
