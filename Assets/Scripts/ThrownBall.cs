using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrownBall : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    float vel_thresh = 1;
    float vel_scale = 35.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame

    public void adjust_vel(){

        Debug.Log(m_Rigidbody.velocity.magnitude);

        m_Rigidbody.AddForce(new Vector3(0.0f, vel_scale, 0.0f));
        Debug.Log("Added Force");

    }
}
