using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SyncVar] string message;
        private PlayerData _playerData;
        

        // Use this for initialization
        void Start()
        {
            _playerData = GetComponent<PlayerData>();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("update");
            if (isLocalPlayer)
            {
                //Debug.Log("localplayer");
                if (_playerData.IsMoving)
                {
                    Debug.Log("ismoving");
                    Player player = PlayerTable.Select(_playerData.playerControllerId);
                    Character c = CharacterTable.Select(_playerData.Character_ID);
                    GameObject moveBlock = Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(_playerData.MovePosition);
                    float distance = Vector3.Distance(_playerData.transform.position, moveBlock.transform.position);
                    float Speed = Vector3.Distance(_playerData.transform.position, moveBlock.transform.position) / 60f;
                    if (distance > c.Stamina*10)
                    {
                        this.message = "Too long";
                        Debug.Log("too long");
                    }
                    else
                    {
                        Debug.Log("ok");
                        float step = Speed * Time.deltaTime;
                        while (_playerData.GridPosition != _playerData.MovePosition)
                        {
                            Debug.Log("moving");
                            _playerData.GridPosition = Vector3.MoveTowards(_playerData.GridPosition, _playerData.MovePosition, step);
                        }

                        _playerData.IsMoving = false;
                    }
                }
                else
                {
                    Debug.Log("else");
                }
                return;
            }
        }

        [Command]
        public void CmdMovement(int id)
        {
            if (_playerData.IsMoving)
            {
                Debug.Log("ismoving");
                Player player = PlayerTable.Select(id);
                Character c = CharacterTable.Select(player.ID);
                GameObject moveBlock = Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(_playerData.MovePosition);
                int distance = int.Parse(Vector3.Distance(_playerData.transform.position, moveBlock.transform.position).ToString());

                if (distance > c.Stamina)
                {
                    this.message = "Too long";
                    Debug.Log("too long");
                }
                else
                {
                    Debug.Log("ok");
                    float step = 1 * Time.deltaTime;
                    while (_playerData.GridPosition != _playerData.MovePosition)
                    {
                        Debug.Log("moving");
                        _playerData.GridPosition = Vector3.MoveTowards(_playerData.GridPosition, _playerData.MovePosition, step);
                    }

                    _playerData.IsMoving = false;
                }
            }   
//            this.transform.position
        }
    }
}