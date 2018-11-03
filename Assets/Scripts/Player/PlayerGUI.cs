using System.Collections;
using System.Collections.Generic;
using Hackathon;
using UnityEngine;

public class PlayerGUI : MonoBehaviour {
    private Texture2D healthGreen;
    private Texture2D healthBlack;

    private Camera _camera;
    private PlayerData _playerData;

    // Use this for initialization
    void Start() {
        _camera = Camera.main;
        _playerData = GetComponent<PlayerData>();

        healthGreen = new Texture2D(1, 1);
        healthGreen.SetPixel(0, 0, Color.green);
        healthGreen.Apply();

        healthBlack = new Texture2D(1, 1);
        healthBlack.SetPixel(0, 0, Color.black);
        healthBlack.Apply();
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnGUI() {
        if (_playerData.PlayerInstance == null)
            return;
        
        Vector3 pos = _camera.WorldToScreenPoint(transform.position + new Vector3(0f, 2.25f, 0f));

        GUI.DrawTexture(new Rect(pos.x - 32f, Screen.height - pos.y, 64f, 8f), healthBlack);
        GUI.DrawTexture(new Rect(pos.x - 32f, Screen.height - pos.y, 32f, 8f), healthGreen);
    }
}