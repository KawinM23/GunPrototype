using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressParticle : MonoBehaviour
{

    Camera mc;
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        mc = GameObject.Find("Main Camera").GetComponent<Camera>();
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    public void Press(Vector3 position) {

    }
}
