using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon {
    public class PlayerMovement : NetworkBehaviour {
        [SyncVar]
        string message;

        private PlayerData _playerData;

        // Use this for initialization
        void Start() {
            _playerData = GetComponent<PlayerData>();
        }

        // Update is called once per frame
        void Update() {
            //Debug.Log("update");
            if (!isLocalPlayer) {
                return;
            }

            if (_playerData.Character_ID != 0 && _playerData.PlayerInstance == null) {
                _playerData.PlayerInstance = PlayerTable.Select(_playerData.playerControllerId);
                _playerData.CharacterInstance = CharacterTable.Select(_playerData.Character_ID);
            }

            if (Input.GetMouseButtonDown(0) && !_playerData.IsMoving) {
                _playerData.MovePosition = Manager.MANAGER.GetComponent<Map>()
                                                  .GetGridPositionByBlock(Manager
                                                                          .MANAGER.GetComponent<MapBlockBehaviour>()
                                                                          .SelectedBlock);
                _playerData.IsMoving = true;
            }

            float step = _playerData.Speed * Time.deltaTime;
            if (_playerData.IsMoving) {
                GameObject moveBlock = Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(_playerData.MovePosition);
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
                            transform.position = moveBlock.transform.position;
                            _playerData.GridPosition = _playerData.MovePosition;
                            _playerData.IsMoving = false;
                        }
                    }
                }
            }
        }

        [Command]
        public void CmdMovement(int id) {
            if (_playerData.IsMoving) {
                Debug.Log("ismoving");
                Player player = PlayerTable.Select(id);
                Character c = CharacterTable.Select(player.ID);
                GameObject moveBlock = Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(_playerData.MovePosition);
                int distance = int.Parse(Vector3.Distance(_playerData.transform.position, moveBlock.transform.position)
                                                .ToString());

                if (distance > c.Stamina) {
                    this.message = "Too long";
                    Debug.Log("too long");
                }
                else {
                    Debug.Log("ok");
                    float step = 1 * Time.deltaTime;
                    while (_playerData.GridPosition != _playerData.MovePosition) {
                        Debug.Log("moving");
                        _playerData.GridPosition =
                            Vector3.MoveTowards(_playerData.GridPosition, _playerData.MovePosition, step);
                    }

                    _playerData.IsMoving = false;
                }
            }

//            this.transform.position
        }
    }
}