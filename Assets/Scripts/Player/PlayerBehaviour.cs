using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon {
    public class PlayerBehaviour : NetworkBehaviour {
        private PlayerData _playerData;
        private Camera     _camera;

        // Use this for initialization
        void Start() {
            _playerData = GetComponent<PlayerData>();
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update() {
            transform.GetChild(0).LookAt(_camera.transform.position);
            transform.GetChild(0).GetComponent<MeshRenderer>().material = Map.MAP.GetComponent<CharacterUtils>()
                                                                             .CharacterMaterials
                                                                                 [_playerData.Character_ID - 1];

            if (!isLocalPlayer)
                return;

            if (!_playerData.IsMoving) {
                transform.position =
                    Map.MAP.GetComponent<Map>().GetBlockOnPosition(_playerData.GridPosition).transform.position;
            }
        }
    }
}