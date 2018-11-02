using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Manager : NetworkManager {
    public static GameObject MANAGER;

    // Use this for initialization
    void Start() {
        MANAGER = this.gameObject;
    }

    // Update is called once per frame
    void Update() {
    }

    public override void OnStartServer() {
        base.OnStartServer();
        {
        }
    }
}