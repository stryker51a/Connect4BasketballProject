using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class ReactionScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    //GameObject obstacle;
    //public GameObject myPrefab;
    
    BoardManager boardManager;
    GameObject[,] lights = new GameObject[6, 7];

    private long ball1_timeout = 0;
    private long ball2_timeout = 0;

    void Start()
    {
        for (int i = 0; i < 6; ++i){
            for (int j = 0; j < 7; ++j){
                // rows i columns j
                string indexTag = i.ToString() + j.ToString();
                lights[i, j] = GameObject.FindGameObjectWithTag(indexTag);
                lights[i, j].GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
        
        //obstacle = GameObject.FindGameObjectWithTag("Obstacle");
        boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collider){
        GameObject obstacle = GameObject.FindGameObjectWithTag(collider.gameObject.tag);
        int ball_num = 1;
        Debug.Log("Tag: ");
        Debug.Log(obstacle.tag);
        if (obstacle.tag != "basketball"){
            ball_num = 2;
        }

        Debug.Log("Calling collider: ");
        Debug.Log(PhotonNetwork.IsMasterClient);

        if (PhotonNetwork.IsMasterClient || !(PhotonNetwork.InRoom)){
            // if (PhotonNetwork.IsMasterClient){
            //     obstacle.GetComponent<PhotonView>().RequestOwnership();
            // }

            // check timeouts
            long milliseconds_now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long timeout = 400;

            Debug.Log(milliseconds_now);

            if (ball_num == 1){
                if (Math.Abs(milliseconds_now - ball1_timeout) < timeout){
                    Debug.Log("return");
                    return;
;                } else {
                    ball1_timeout = milliseconds_now;
                }
            } else {
                if (Math.Abs(milliseconds_now - ball2_timeout) < timeout){
                    Debug.Log("return");
                    return;
                } else {
                    ball2_timeout = milliseconds_now;
                }
            }
            Debug.Log("we are yeeting.");


            // find the column

            string colStr = this.tag;
            int col = 0;
            while(col.ToString() != colStr){
                ++col;
            }
            int row = 0;
            for(int i = 5; i >= 0; --i){
                if(lights[i, col].GetComponent<MeshRenderer>().material.color == Color.blue){
                    //lights[i, col].GetComponent<MeshRenderer>().material = obstacle.GetComponent<MeshRenderer>().material;
                    //Material m1 = obstacle.GetComponent<MeshRenderer>().material;
                    Debug.Log("Setting light");
                    if(PhotonNetwork.InRoom){
                        this.photonView.RPC("setLight", RpcTarget.AllBuffered, collider.gameObject.tag, i, col);
                    } else {
                        lights[i, col].GetComponent<MeshRenderer>().material = obstacle.GetComponent<MeshRenderer>().material;
                    }

                    row = i;
                    break;
                }
            }

            Debug.Log("Checking game over");
            bool gameOver = connectFour(row, col, obstacle);
            if(gameOver){
                //lights[0,0].GetComponent<MeshRenderer>().material.color = Color.green;
                //winnerScreen.enabled = true;
                //winnerScreenObj.SetActive(true);
                string winnerColor = "";
                if(obstacle.tag == "Obstacle"){
                    winnerColor = "yellow";
                } else {
                    winnerColor = "red";
                }
                if(PhotonNetwork.InRoom){
                    this.photonView.RPC("setGameOver", RpcTarget.AllBuffered, true, winnerColor);
                } else {
                    boardManager.GameOver(true, winnerColor);
                }
                
                //boardManager.GameOver(true, winnerColor);
                return;
            } else {
                //lights[0,0].GetComponent<MeshRenderer>().material.color = Color.black;
                //winnerScreen.enabled = false;
                //winnerScreenObj.SetActive(false);

                if (PhotonNetwork.InRoom){
                    this.photonView.RPC("moveBalls", RpcTarget.AllBuffered, collider.gameObject.tag);
                }
                else {
                    if(obstacle.tag == "basketball"){
                        Debug.Log("Moving red ball");
                        obstacle.transform.position = new Vector3(10.0f, 1.0f, 1.0f);
                        obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    } else {
                        Debug.Log("Moving yellow ball");
                        //obstacle.transform.position = new Vector3(10.12f, 1.0f, 5.453f);
                        obstacle.transform.position = new Vector3(10.12f, 1.0f, 5.453f);
                        obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    }
                }
                
                return;
            }
        }
        //Destroy(obstacle);
        //Instantiate(myPrefab, new Vector3(-6, 1, -10), Quaternion.identity);

    }

    // private void OnCollisionEnter(Collision collider){
    //     GameObject obstacle = GameObject.FindGameObjectWithTag(collider.gameObject.tag);
    //     Debug.Log("Calling collider: ");
    //     Debug.Log(PhotonNetwork.IsMasterClient);

    //     // find the column

    //     string colStr = this.tag;
    //     int col = 0;
    //     while(col.ToString() != colStr){
    //         ++col;
    //     }
    //     int row = 0;
    //     for(int i = 5; i >= 0; --i){
    //         if(lights[i, col].GetComponent<MeshRenderer>().material.color == Color.blue){
    //             //lights[i, col].GetComponent<MeshRenderer>().material = obstacle.GetComponent<MeshRenderer>().material;
    //             //Material m1 = obstacle.GetComponent<MeshRenderer>().material;
    //             Debug.Log("Setting light");
    //             if(PhotonNetwork.InRoom){
    //                 this.photonView.RPC("setLight", RpcTarget.AllBuffered, collider.gameObject.tag, i, col);
    //             } else {
    //                 lights[i, col].GetComponent<MeshRenderer>().material = obstacle.GetComponent<MeshRenderer>().material;
    //             }
                

    //             row = i;
    //             break;
    //         }
    //     }

    //     Debug.Log("Checking game over");
    //     bool gameOver = connectFour(row, col, obstacle);
    //     if(gameOver){
    //         //lights[0,0].GetComponent<MeshRenderer>().material.color = Color.green;
    //         //winnerScreen.enabled = true;
    //         //winnerScreenObj.SetActive(true);
    //         string winnerColor = "";
    //         if(obstacle.tag == "Obstacle"){
    //             winnerColor = "yellow";
    //         } else {
    //             winnerColor = "red";
    //         }
    //         if(PhotonNetwork.InRoom){
    //             this.photonView.RPC("setGameOver", RpcTarget.AllBuffered, true, winnerColor);
    //         } else {
    //             boardManager.GameOver(true, winnerColor);
    //         }
            
    //         //boardManager.GameOver(true, winnerColor);
    //         return;
    //     } else {
    //         //lights[0,0].GetComponent<MeshRenderer>().material.color = Color.black;
    //         //winnerScreen.enabled = false;
    //         //winnerScreenObj.SetActive(false);

    //         if (PhotonNetwork.InRoom){
    //             this.photonView.RPC("moveBalls", RpcTarget.AllBuffered, collider.gameObject.tag);
    //         }
    //         else {
    //             if(obstacle.tag == "basketball"){
    //                 Debug.Log("Moving red ball");
    //                 obstacle.transform.position = new Vector3(10.0f, 1.0f, 1.0f);
    //                 obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //                 obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //             } else {
    //                 Debug.Log("Moving yellow ball");
    //                 //obstacle.transform.position = new Vector3(10.12f, 1.0f, 5.453f);
    //                 obstacle.transform.position = new Vector3(10.12f, 1.0f, 5.453f);
    //                 obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //                 obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //             }
    //         }
            
    //         return;
            
    //     }
    //     //Destroy(obstacle);
    //     //Instantiate(myPrefab, new Vector3(-6, 1, -10), Quaternion.identity);

    // }

    [PunRPC]
    public void setLight(string collider_tag, int row, int col){
        Debug.Log("Setting Light color");
        GameObject obstacle2 = GameObject.FindGameObjectWithTag(collider_tag);
        lights[row, col].GetComponent<MeshRenderer>().material = obstacle2.GetComponent<MeshRenderer>().material;
    }

    [PunRPC]
    public void setGameOver(bool status, string winnerColor){
        Debug.Log("setting Game Over");
        boardManager.GameOver(true, winnerColor);
    }

    [PunRPC]
    public void moveBalls(string tag_1){
        GameObject obstacle = GameObject.FindGameObjectWithTag(tag_1);
        if(obstacle.tag == "basketball"){
            Debug.Log("Moving red ball");
            obstacle.transform.position = new Vector3(10.0f, 1.0f, 1.0f);
            obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        } else {
            Debug.Log("Moving yellow ball");
            //obstacle.transform.position = new Vector3(10.12f, 1.0f, 5.453f);
            obstacle.transform.position = new Vector3(10.12f, 1.0f, 5.453f);
            obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }


    private bool fourInARow(int row, GameObject obstacle){
        int inARow = 0;
        
        for(int i = 0; i < 6; ++i){
            if (lights[row, i].GetComponent<MeshRenderer>().material.color == obstacle.GetComponent<MeshRenderer>().material.color){
                ++inARow;
                if(inARow >= 4){
                    return true;
                }
            } else {
                inARow = 0;
            }
        }
    
        return false;
    }

    private bool fourInACol(int col, GameObject obstacle){
        int inARow = 0;
        // find first
        for(int i = 0; i < 6; ++i){
            if (lights[i, col].GetComponent<MeshRenderer>().material.color == obstacle.GetComponent<MeshRenderer>().material.color){
                ++inARow;
                if(inARow >= 4){
                    return true;
                }
            } else {
                inARow = 0;
            }
        }
        return false;
    }

    private bool negativeDiag(int row, int col, GameObject obstacle){
        int inARow = 0;
        for(int i = 3; i >= -3; --i){
            if((row - i >= 0 && row - i < 6) && (col - i >= 0 && col - i < 7)){
                // row and col in bounds
                if (lights[row - i, col - i].GetComponent<MeshRenderer>().material.color == obstacle.GetComponent<MeshRenderer>().material.color){
                    ++inARow;
                    if(inARow == 4){
                        return true;
                    }
                } else {
                    inARow = 0;
                }
            }
        }
        return false;
    }

    private bool positiveDiag(int row, int col, GameObject obstacle){
        int inARow = 0;
        for(int i = 3; i >= -3; --i){
            if((row - i >= 0 && row - i < 6) && (col + i >= 0 && col + i < 7)){
                // row and col in bounds
                if (lights[row - i, col + i].GetComponent<MeshRenderer>().material.color == obstacle.GetComponent<MeshRenderer>().material.color){
                    ++inARow;
                    if(inARow == 4){
                        return true;
                    }
                } else {
                    inARow = 0;
                }
            }
        }
        return false;
    }

    private bool connectFour(int row, int col, GameObject obstacle){
        bool inARow = fourInARow(row, obstacle);
        if(!inARow){
            bool inACol = fourInACol(col, obstacle);
            if(!inACol){
                bool negDiag = negativeDiag(row, col, obstacle);
                if(!negDiag){
                    bool posDiag = positiveDiag(row, col, obstacle);
                    if(!posDiag){
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return true;
                }
            } else {
                return true;
            }
        } else {
            return true;
        }
    }

}

