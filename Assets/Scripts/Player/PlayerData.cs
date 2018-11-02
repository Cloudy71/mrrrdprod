using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour {
    [SyncVar]
    public string Name;

    [SyncVar]
    public int Armor;

    [SyncVar]
    public int Health;

    [SyncVar]
    public int Score;

    [SyncVar]
    public int Inventory_ID;

    [SyncVar]
    public int Character_ID;

    [SyncVar]
    public Vector2 GridPosition;

    [SyncVar]
    public Vector2 MovePosition;


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }
}