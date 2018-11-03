using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Hackathon
{
    public class BulletData : NetworkBehaviour {

        [SyncVar]
        public bool isMoving;

        [SyncVar]
        public int Speed = 1;

        [SyncVar]
        public int AttackerId;

        [SyncVar]
        public int DefenderId;

        [SyncVar]
        public Vector2 GridPosition;

        [SyncVar]
        public Vector2 MovePosition;


        void Start()
        {
            Debug.Log("Bullet");

        }

        // Update is called once per frame
        void Update() {
            Debug.Log("update");
            float step = Speed * Time.deltaTime;
            if (isMoving)
            {
                Debug.Log("ismoving");
                GameObject moveBlock = Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(MovePosition);

                if (GridPosition != MovePosition)
                {
                    //Debug.Log("moving");
                    transform.position = Vector3.MoveTowards(transform.position, moveBlock.transform.position, step);
                    if (Vector3.Distance(transform.position, moveBlock.transform.position) < 0.1f)
                    {
                        transform.position = moveBlock.transform.position;
                        GridPosition = MovePosition;
                        PlayerCombat.DoPlayerCombat(AttackerId, DefenderId);
                        isMoving = false;
                    }
                }
            }
        }
    }
}
