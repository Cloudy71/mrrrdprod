using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon {
    public class PlayerMovement : NetworkBehaviour {
        [SyncVar]
        string message;

        private PlayerData _playerData;

        private int _oldCharId;

        // Use this for initialization
        void Start() {
            _playerData = GetComponent<PlayerData>();
        }

        // Update is called once per frame
        void Update() {
            //Debug.Log("update");

            if (_playerData.Character_ID != 0 &&
                (_playerData.CharacterInstance == null || _oldCharId != _playerData.Character_ID)) {
                _playerData.PlayerInstance = PlayerTable.Select(_playerData.playerControllerId);
                _playerData.CharacterInstance = CharacterTable.Select(_playerData.Character_ID);
                _oldCharId = _playerData.Character_ID;
                Debug.Log("Refresh");
            }

            if (!isLocalPlayer) {
                return;
            }

            if (Input.GetMouseButtonDown(0) && !_playerData.IsMoving) {
                _playerData.MovePosition = Map.MAP.GetComponent<Map>()
                                              .GetGridPositionByBlock(Map.MAP.GetComponent<MapBlockBehaviour>()
                                                                         .SelectedBlock);
                _playerData.IsMoving = true;
            }

            float step = _playerData.Speed * Time.deltaTime;
            if (_playerData.IsMoving) {
                GameObject moveBlock = Map.MAP.GetComponent<Map>().GetBlockOnPosition(_playerData.MovePosition);
                float distance = Vector3.Distance(_playerData.transform.position, moveBlock.transform.position);


                if (distance > _playerData.CharacterInstance.Stamina * 10) {
                    this.message = "Too long";
                }
                else {
                    if (_playerData.GridPosition != _playerData.MovePosition) {
                        Debug.Log("moving");
                        transform.position =
                            Vector3.MoveTowards(transform.position, moveBlock.transform.position, step);
                        if (Vector3.Distance(transform.position, moveBlock.transform.position) < 0.1f) {
                            _playerData.GridPosition = _playerData.MovePosition;
                        }
                    }
                    else {
                        transform.position = moveBlock.transform.position;
                        _playerData.IsMoving = false;
                    }
                }
            }
        }
    }
}