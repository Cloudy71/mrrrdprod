using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class BulletData : NetworkBehaviour
    {

        [SyncVar]
        public bool isMoving;

        [SyncVar]
        public float Speed = 10f;

        [SyncVar]
        public Transform Attacker;

        [SyncVar]
        public Transform Defender;

        [SyncVar]
        public Vector2 GridPosition;

        [SyncVar]
        public Vector2 MovePosition;


        void Start()
        {
            Debug.Log("Bullet");

        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("update");
            float step = Speed * Time.deltaTime;
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            Debug.Log("ismoving");
            //Debug.Log("moving");
            transform.position = Vector3.MoveTowards(transform.position, Defender.position, step);
            if (Vector3.Distance(transform.position, Defender.position) < 0.1f)
            {
                Attacker.GetComponent<PlayerCombat>().DoPlayerCombat(Defender);
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}
