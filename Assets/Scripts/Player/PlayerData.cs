using System.Collections;
using System.Collections.Generic;
using Hackathon;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon {
    public class PlayerData : NetworkBehaviour {
        [SyncVar]
        public string Name;

        [SyncVar]
        public int Armor;

        [SyncVar]
        public int Health;

        [SyncVar]
        public int Score;

        [SyncVar]
        public int Inventory_ID;

        [SyncVar]
        public int Character_ID;

        [SyncVar]
        public Vector2 GridPosition;

        [SyncVar]
        public Vector2 MovePosition;

        [SyncVar]
        public bool IsMoving;

        [SyncVar]
        public float Speed = 2f;

        public Player    PlayerInstance;
        public Character CharacterInstance;


        // Use this for initialization
        void Start() {
            Debug.Log("Bullet");
        }

        // Update is called once per frame
        void Update() {
        }
    }
}