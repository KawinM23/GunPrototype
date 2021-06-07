using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool hacking;
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    public void Update() {
        if (!hacking && Time.timeScale != 1f) {
            if (Time.timeScale < 0.96f) {
                Time.timeScale += (1f / slowdownFactor) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            } else {
                Time.timeScale = 1f;
            }
            
            
        }
    }

    public void DoSlowmotion() {
        hacking = true;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
