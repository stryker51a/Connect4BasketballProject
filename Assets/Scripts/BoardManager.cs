using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    GameObject winnerScreen;
    public GameObject winnerText;
    Connect4Manager connect4Manager;

    // Start is called before the first frame update
    void Start()
    {
        connect4Manager = Connect4Manager.Instance;
        winnerScreen = GameObject.Find("Canvas");

        winnerScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(bool over, string color){
        Debug.Log("Game Over");
        winnerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Game Over. Winner " + color + "!";
        winnerScreen.SetActive(over);
        if (over){
            connect4Manager.set_hand_interactors(false);
        }
    }
}
