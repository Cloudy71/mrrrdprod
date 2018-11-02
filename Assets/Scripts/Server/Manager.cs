using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Manager : NetworkManager {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnStartServer()
    {
        base.OnStartServer();
        {

        }
        string comm;
        Hackathon.QA_class.Create(out comm);
        Debug.Log(comm);
    }
}
