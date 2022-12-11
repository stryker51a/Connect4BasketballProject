using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedBallOnGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collider){
        GameObject obstacle = GameObject.FindGameObjectWithTag(collider.gameObject.tag);
        if(obstacle.tag == "basketball"){
            obstacle.transform.position = new Vector3(10, 1, 1);
            obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        } else {
            obstacle.transform.position = new Vector3(10.12f, 1.0f, 5.453f);
            obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
