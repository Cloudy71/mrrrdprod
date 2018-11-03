using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{

    public class BulletMove : MonoBehaviour
    {

        private BulletData BulletData;

        void Start()
        {
            BulletData = GetComponent<BulletData>();
        }

        void Update()
        {
            float step = BulletData.Speed * Time.deltaTime;
            if (BulletData.isMoving)
            {
                GameObject moveBlock = Manager.MANAGER.GetComponent<Map>().GetBlockOnPosition(BulletData.MovePosition);

                if (BulletData.GridPosition != BulletData.MovePosition)
                {
                    //Debug.Log("moving");
                    transform.position = Vector3.MoveTowards(transform.position, moveBlock.transform.position, step);
                    if (Vector3.Distance(transform.position, moveBlock.transform.position) < 0.1f)
                    {
                        transform.position = moveBlock.transform.position;
                        BulletData.GridPosition = BulletData.MovePosition;
                        PlayerCombat.DoPlayerCombat(BulletData.AttackerId, BulletData.DefenderId);
                        BulletData.isMoving = false;
                        Destroy(this);
                    }
                }
            }
        }
    }
}
