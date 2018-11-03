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

    public Block.BlockType GetBlockTypeOnPosition(Vector2 position) {
        return GetBlockOnPosition(position).GetComponent<Block>().Type;
    }

    public Vector2 GetGridPositionByBlock(GameObject block) {
        GameObject obj = null;
        for (int i = 0; i < Blocks.Count; i++) {
            if (Blocks[i].Equals(block)) {
                int x = (int) (i / Size.x);
                int y = i - (int) (x * Size.x);
                return new Vector2(x, y);
            }
        }

        return Vector2.zero;
    }
}