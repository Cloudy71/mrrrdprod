using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon {
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
                    Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(_playerData.GridPosition).transform.position;
            }

            transform.GetChild(0).LookAt(_camera.transform.position);
            transform.GetChild(0).GetComponent<MeshRenderer>().material = Manager
                                                                          .MANAGER.GetComponent<CharacterUtils>()
                                                                          .CharacterMaterials
                                                                              [_playerData.Character_ID - 1];
        }
    }
}