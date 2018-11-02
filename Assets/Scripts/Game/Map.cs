using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    [HideInInspector]
    public List<GameObject> Blocks;

    public Vector2 Size;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public GameObject GetBlockOnPosition(Vector2 position) {
        int idx = (int) (position.x * Size.y + position.y);
        return Blocks[idx];
    }

    public GameObject GetBlockOnPosition(int x, int y) {
        return GetBlockOnPosition(new Vector2(x, y));
    }
}