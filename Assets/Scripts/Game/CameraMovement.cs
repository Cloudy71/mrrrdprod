using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Map _map;

    private Camera _camera;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(1)) {
            Debug.Log("Right mouse button...");
        }
    }
}