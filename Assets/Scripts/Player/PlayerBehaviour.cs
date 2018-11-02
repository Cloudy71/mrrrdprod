using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    private PlayerData _playerData;
    private Camera     _camera;

    // Use this for initialization
    void Start() {
        _playerData = GetComponent<PlayerData>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        if (!_playerData.IsMoving) {
            transform.position =
                Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(_playerData.GridPosition).transform.position +
                new Vector3(0.5f, 0.75f, 0.5f);
        }

        transform.LookAt(_camera.transform.position);
    }
}