using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRPCScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Helper helper;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test_rpc(){
        helper.end_turn();
    }
}
