using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon {
    public class PlayerMovement : NetworkBehaviour {
        [SyncVar]
        string message;

        private PlayerData _playerData;
        private PlayerData _playerDataD;
        private GameObject[] players;

        // Use this for initialization
        void Start() {
            _playerData = GetComponent<PlayerData>();
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        // Update is called once per frame
        void Update() {
            //Debug.Log("update");
            if (!isLocalPlayer) {
                return;
            }

            if (_playerData.Character_ID != 0 && _playerData.CharacterInstance == null) {
                _playerData.PlayerInstance = PlayerTable.Select(_playerData.playerControllerId);
                _playerData.CharacterInstance = CharacterTable.Select(_playerData.Character_ID);
                Debug.Log("aaa");
            }

            if (Input.GetMouseButtonDown(0) && !_playerData.IsMoving)
            {
                _playerData.MovePosition = Map.MAP.GetComponent<Map>()
                                                  .GetGridPositionByBlock(Map
                                                                          .MAP.GetComponent<MapBlockBehaviour>()
                                                                          .SelectedBlock);
                GameObject moveBlock = Map.MAP.GetComponent<Map>().GetBlockOnPosition(_playerData.MovePosition);
                players = GameObject.FindGameObjectsWithTag("Player");
                bool shoot = false;
                foreach (GameObject p in players)
                {
                    Debug.Log(p.transform.position + " - " + moveBlock.transform.position);
                    if (Vector3.Distance(p.transform.position, moveBlock.transform.position) < 1f && p.name !=_playerData.name)
                    {
                        _playerDataD = p.GetComponent<PlayerData>();
                        shoot = true;
                        break;
                    }
                }

                if (!shoot)
                {
                    Debug.Log("if");
                    _playerData.IsMoving = true;
                }
                else
                {
                    Destroy(_playerDataD.gameObject);
                    Debug.Log("else");
                    PlayerBehaviour.RunCmdFire(_playerData.playerControllerId, _playerDataD.playerControllerId, _playerData.transform.position, _playerDataD.transform.position);
                }

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
                        //Debug.Log("moving");
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
    }
}