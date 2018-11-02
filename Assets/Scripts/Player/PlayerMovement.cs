using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class PlayerMovement : NetworkBehaviour
    {

        private PlayerData _playerData;
        // Use this for initialization
        void Start()
        {
            _playerData = GetComponent<PlayerData>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        [Command]
        public void CmdRefill(int id)
        {
            Player player = PlayerTable.Select(id);
            System.Random rnd = new System.Random();
            int rnum = rnd.Next(0, 100);
            GameObject moveBlock = Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(_playerData.MovePosition);

            _playerData.GridPosition = _playerData.MovePosition;
            _playerData.IsMoving = false;
//            this.transform.position
        }
    }
}