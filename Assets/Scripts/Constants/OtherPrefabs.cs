using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hackathon
{
    public class OtherPrefabs : MonoBehaviour
    {

        public GameObject BulletPrefab;

        // Use this for initialization
        void Start()
        {
            Instantiate(BulletPrefab);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
