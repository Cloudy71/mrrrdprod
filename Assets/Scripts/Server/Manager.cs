using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Manager : NetworkManager {

    Hackathon.PlayerSelection ps = new Hackathon.PlayerSelection();
    Hackathon.WeaponsFiller wp = new Hackathon.WeaponsFiller();

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

        // db věci..
        Debug.Log("server started..");
       
        wp.Create();
        ps.CmdCharacterSelection("hovno", 1);
    }
}
