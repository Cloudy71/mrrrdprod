using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{

    public class BulletMove : MonoBehaviour
    {

        private PlayerData _playerData;
        private GameObject[] players;

        void Start()
        {

            players = GameObject.FindGameObjectsWithTag("Player");
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_playerData.IsMoving)
            {
                _playerData.MovePosition = Manager.MANAGER.GetComponent<Map>()
                                                  .GetGridPositionByBlock(Manager
                                                                          .MANAGER.GetComponent<MapBlockBehaviour>()
                                                                          .SelectedBlock);
                _playerData.IsMoving = true;
            }

        }
    }
}
