using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject menuMenu;
    [SerializeField] private Text timer;
    [SerializeField] private AudioSource musicSource;

    private bool inLevel;
    public bool start;
    public static bool isPause;
    private float tempTimeScale;

    public bool hacking;
    public float slowdownFactor;
    public float slowdownLength;

    public float timePass;

    private bool die;

    private void Start() {
        
        isPause = false;
        start = false;
        die = false;
        Time.timeScale = 1;

        timePass = 0;
        inLevel = LevelManager.InLevel();
        pauseMenu.SetActive(false);
        menuMenu.SetActive(false);

        if (!inLevel || SceneManager.GetActiveScene().name.Contains("Tutorial")) {
            timer.gameObject.SetActive(false);
        }
    }

    public void Update() {
        if (inLevel && !isPause) {
            if (!hacking && Time.timeScale != 1f && !die) {
                if (Time.timeScale < 0.98f) {
                    Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
                    Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
                } else {
                    Time.timeScale = 1f;
                }
            }
            if (start) {
                timePass += Time.deltaTime;
                timer.text = "Time : " + timePass.ToString("0.0");
            }
        }
        if (!die &&Input.GetKeyDown(KeyCode.Escape) && LevelManager.InLevel()) {
            TogglePause();
        }
        if (!die && Input.GetKeyDown(KeyCode.Escape) && !LevelManager.InLevel()) {
            ToggleMenu();
        }

    }

    public void StartMoving() {
        start = true;
    }

    public void DoSlowmotion() {
        hacking = true;
        Time.timeScale = slowdownFactor;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public IEnumerator DoSlowmotionDie() {
        die = true;
        float time = 0;
        while(Time.timeScale > 0.1f) {
            Time.timeScale = Mathf.Lerp(1, 0, time);
            time += Time.unscaledDeltaTime/1.5f;
            yield return null;
        }
        yield return null;
    }

    public void Pause() {
        isPause = true;
        tempTimeScale = Time.timeScale;
        Time.timeScale = 0;
        musicSource.Pause();
        pauseMenu.SetActive(true);
    }

    public void Resume() {
        isPause = false;
        Time.timeScale = tempTimeScale;
        musicSource.UnPause();
        pauseMenu.SetActive(false);
    }

    public void TogglePause() {
        if (isPause) {
            Resume();
        } else {
            Pause();
        }
    }

    public void ToggleMenu() {
        if (menuMenu.activeSelf) {
            menuMenu.SetActive(false);
        } else {
            menuMenu.SetActive(true);
        }
    }



}
