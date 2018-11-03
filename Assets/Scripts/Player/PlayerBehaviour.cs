using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon {
    public class PlayerBehaviour : NetworkBehaviour {
        private PlayerData _playerData;
        private Camera     _camera;
        public GameObject BulletPrefab;

        // Use this for initialization
        void Start() {
            _playerData = GetComponent<PlayerData>();
            _camera = Camera.main;
            BulletPrefab = Instantiate(BulletPrefab);
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
        [Command]
        public void CmdFire(int attackerID, int defenderID, Vector2 Gridposition, Vector2 Moveposition)
        {
            Instantiate(Map.MAP.GetComponent<OtherPrefabs>().BulletPrefab, transform);
        }

        public static void RunCmdFire(int attackerID, int defenderID, Vector2 Gridposition, Vector2 Moveposition)
        {
            PlayerSelection.LocalPlayer.GetComponent<PlayerBehaviour>().CmdFire(attackerID, defenderID, Gridposition, Moveposition);
        }
    }
}