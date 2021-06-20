using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    Camera tcm;
    CinemachineVirtualCamera cm;
    BoxCollider2D triggerArea;

    private void Awake() {

        cm = GetComponent<CinemachineVirtualCamera>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        tcm = GameObject.Find("TrackingCM").GetComponent<Camera>();
        triggerArea = transform.GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision != null && collision.CompareTag("Player")) {
            Debug.Log("Enter");
            cm.Priority = 15;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision != null && collision.CompareTag("Player")) {
            cm.Priority = 5;
        }
    }
}
