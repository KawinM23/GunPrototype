using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Camera mainCemera;
    Vector3 screenPoint;
    bool inScreen;

    private void Start() {
        mainCemera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    private void Update() {
        screenPoint = mainCemera.WorldToViewportPoint(transform.position);
        inScreen = screenPoint.z > -10 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        Debug.Log(inScreen);
    }
}
