﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public GameObject HexagonPrefab;
    public int        Width;
    public int        Height;
    public Material   GroundMaterial;
    public Material   WallMaterial;

    private Map _map;

    public static readonly MapOffset[] MapOffsets = new MapOffset[] {
        new MapOffset(1f, 0f),        // Right
        new MapOffset(-1f, 0f),       // Left
        new MapOffset(-0.5f, 0.75f),  // TopLeft
        new MapOffset(0.5f, 0.75f),   // TopRight
        new MapOffset(-0.5f, -0.75f), // BottomLeft
        new MapOffset(0.5f, -0.75f),  // BottomRight
        new MapOffset(0f, 0f),        // Empty 
    };

    // Use this for initialization
    void Start() {
        _map = GetComponent<Map>();
        _map.Size = new Vector2(Width, Height);
        _map.Blocks = new List<GameObject>();

        for (int i = 0; i < Width; i++) {
            bool left = false;
            for (int j = 0; j < Height; j++) {
                Vector3 pos = new Vector3(i * MapOffsets[0].x + (left ? MapOffsets[4].x : 0f),
                                          0f,
                                          j * MapOffsets[4].y);
                GameObject gObject = Instantiate(HexagonPrefab, pos, HexagonPrefab.transform.rotation);
                Block block = gObject.GetComponent<Block>();
                if (i == 0 || i == Width - 1 || j == 0 || j == Height - 1) {
                    gObject.GetComponent<MeshRenderer>().material = WallMaterial;
                    block.Type = Block.BlockType.Solid;
                }
                else {
                    gObject.GetComponent<MeshRenderer>().material = GroundMaterial;
                    block.Type = Block.BlockType.Moveable;
                }

                left = !left;
                _map.Blocks.Add(gObject);
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }

    public class MapOffset {
        public readonly float x;
        public readonly float y;

        public MapOffset(float x, float y) {
            this.x = x;
            this.y = y;
        }
    }
}