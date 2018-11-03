using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public Vector3 Position;
    public float   Zoom = 3f;

    private Map     _map;
    private Camera  _camera;
    private Vector3 _dragStart;
    private Vector3 _dragPosition;

    // Use this for initialization
    void Start() {
        _camera = Camera.main;
        Position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update() {
        if (!Manager.MANAGER.GetComponent<Manager>().isNetworkActive || Map.MAP == null) {
            return;
        }

        if (_map == null) {
            _map = Map.MAP.GetComponent<Map>();
        }

        if (Input.GetMouseButtonDown(1)) {
            _dragStart = Input.mousePosition;
            _dragPosition = Position;
        }

        if (Input.GetMouseButton(1)) {
            Vector3 dragDiff = (Input.mousePosition - _dragStart) / 100f;
            Position =
                new
                    Vector3(_dragPosition.x - dragDiff.y * Mathf.Sin(Mathf.Deg2Rad * (_camera.transform.eulerAngles.y + 0f)) + dragDiff.x * Mathf.Sin(Mathf.Deg2Rad * (_camera.transform.eulerAngles.y + 0f)),
                            _dragPosition.y,
                            _dragPosition.z + dragDiff.x *
                            Mathf.Sin(Mathf.Deg2Rad * (_camera.transform.eulerAngles.y + 0f)) -
                            dragDiff.y * Mathf.Cos(Mathf.Deg2Rad * (_camera.transform.eulerAngles.y + 0f)));
        }

        Zoom -= Input.mouseScrollDelta.y * 0.25f;
        if (Zoom < 1f)
            Zoom = 1f;

        if (Position.x < 0f) {
            Position.x = 0f;
        }

        if (Position.z < -_map.Size.y) {
            Position.z = -_map.Size.y;
        }

        if (Position.x > _map.Size.x) {
            Position.x = _map.Size.x;
        }

        if (Position.z > 0) {
            Position.z = 0;
        }

        _camera.transform.position = Position + new Vector3(-Zoom, Zoom + 2f, Zoom);
        _camera.transform.LookAt(Position);
    }
}