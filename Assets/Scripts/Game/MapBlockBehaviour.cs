using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlockBehaviour : MonoBehaviour {
    private Map _map;

    private Camera     _camera;
    private GameObject _lastHovered = null;

    // Use this for initialization
    void Start() {
        _map = GetComponent<Map>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        MouseOnBlock();
    }

    private void MouseOnBlock() {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits =
            Physics.RaycastAll(ray, 1000f);

        if (raycastHits.Length > 0) {
            GameObject nearest = raycastHits[0].transform.gameObject;
            float dist = raycastHits[0].distance;
            foreach (RaycastHit raycastHit in raycastHits) {
                if (raycastHit.distance < dist) {
                    dist = raycastHit.distance;
                    nearest = raycastHit.transform.gameObject;
                }
            }

            if (_lastHovered != null) {
                _lastHovered.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.black);
                _lastHovered.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
            }

            _lastHovered = nearest;
            nearest.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.red);
            nearest.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1f, 0.75f, 0.75f));
        }
    }
}