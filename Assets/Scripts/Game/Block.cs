using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Block : NetworkBehaviour {

	public enum BlockType {
		Moveable, Solid, LootBox
	}

	[SyncVar]
	public BlockType Type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
