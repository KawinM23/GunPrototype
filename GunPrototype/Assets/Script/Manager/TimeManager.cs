using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool hacking;
    public float slowdownFactor;
    public float slowdownLength;

    public void Update() {
        if (!hacking && Time.timeScale != 1f) {
            if (Time.timeScale < 0.98f) {
                Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
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
