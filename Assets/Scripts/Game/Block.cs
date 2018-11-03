using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Block : NetworkBehaviour {
    public enum BlockType {
        Moveable,
        Solid,
        LootBox
    }

    public enum BlockMaterialType {
        Ground,
        Wall,
        Dirt
    }

    [SyncVar]
    public BlockType Type = BlockType.Moveable;

    [SyncVar]
    public BlockMaterialType MaterialType = BlockMaterialType.Ground;

    [SyncVar]
    public int ID = -1;

    private Material _oldMaterial;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (Map.MAP.GetComponent<Map>().Generated) {
            Map.MAP.GetComponent<Map>().Blocks[ID] = gameObject;
        }

        Material material = null;
        switch (MaterialType) {
            case BlockMaterialType.Ground:
                material = Manager.MANAGER.GetComponent<MaterialUtils>().GroundMaterial;
                break;
            case BlockMaterialType.Dirt:
                material = Manager.MANAGER.GetComponent<MaterialUtils>().DirtMaterial;
                break;
            case BlockMaterialType.Wall:
                material = Manager.MANAGER.GetComponent<MaterialUtils>().WallMaterial;
                break;
        }

        if (material != _oldMaterial) {
            GetComponent<MeshRenderer>().material = material;
            _oldMaterial = material;
        }
    }
}