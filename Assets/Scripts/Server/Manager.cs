using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;


public class Manager : NetworkManager {
    public static GameObject MANAGER;
    public static GameObject MAP;

    // Use this for initialization
    void Start() {
        MANAGER = this.gameObject;
        MAP = GameObject.Find("MAP");
    }

    // Update is called once per frame
    void Update() {
    }

    public override void OnStartServer() {
        base.OnStartServer();
    }
}