using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool isPause;
    private float tempTimeScale;
    private GameObject PauseMenu;

    public bool hacking;
    public float slowdownFactor;
    public float slowdownLength;

    private void Start() {
        PauseMenu = GameObject.Find("PauseMenu");
        PauseMenu.SetActive(false);
        isPause = false;
    }

    public void Update() {
        if (!isPause) {
            if (!hacking && Time.timeScale != 1f) {
                if (Time.timeScale < 0.98f) {
                    Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
                    Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
                } else {
                    Time.timeScale = 1f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void DoSlowmotion() {
        hacking = true;
        Time.timeScale = slowdownFactor;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void Pause() {
        isPause = true;
        tempTimeScale = Time.timeScale;
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    public void Resume() {
        isPause = false;
        Time.timeScale = tempTimeScale;
        PauseMenu.SetActive(false);
    }

    public void TogglePause() {
        if (isPause) {
            Resume();
        } else {
            Pause();
        }
    }

}
