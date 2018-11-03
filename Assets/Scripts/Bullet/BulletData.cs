using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletData : NetworkBehaviour {


    [SyncVar]
    public int Speed;

    [SyncVar]
    public int AttackerId;

    [SyncVar]
    public int DefenderId;


    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
   
}
