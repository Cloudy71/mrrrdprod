using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Map : NetworkBehaviour {
    public static GameObject MAP;

    [HideInInspector]
    public GameObject[] Blocks;

    [SyncVar]
    public Vector2 Size = Vector2.zero;

    public bool Generated = false;

    // Use this for initialization
    void Start() {
        MAP = gameObject;
    }

    // Update is called once per frame
    void Update() {
        MAP = gameObject;

        if (Size != Vector2.zero && !Generated) {
            Generated = true;
            Blocks = new GameObject[(int) (Size.x * Size.y)];
        }
    }

    public GameObject GetBlockOnPosition(Vector2 position) {
        int idx = (int) (position.x * Size.y + position.y);
       // Debug.Log(idx);
        return Blocks[idx];
    }

    public GameObject GetBlockOnPosition(int x, int y) {
        return GetBlockOnPosition(new Vector2(x, y));
    }

    public Block.BlockType GetBlockTypeOnPosition(Vector2 position) {
        return GetBlockOnPosition(position).GetComponent<Block>().Type;
    }

    public Vector2 GetGridPositionByBlock(GameObject block) {
        GameObject obj = null;
        for (int i = 0; i < Blocks.Length; i++) {
            if (Blocks[i].Equals(block)) {
                int x = (int) (i / Size.x);
                int y = i - (int) (x * Size.x);
                return new Vector2(x, y);
            }
        }

        return Vector2.zero;
    }
}