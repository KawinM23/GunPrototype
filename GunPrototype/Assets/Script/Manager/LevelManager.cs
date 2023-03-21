using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private GameObject player;
    [SerializeField] private TimeManager tm;
    [SerializeField] private GameObject pauseTab;
    [SerializeField] private Image retryCircle;

    private static int playingLevel = 0;

    private float retryTime;
    public float retryTimeRequired;

    [SerializeField] AudioSource musicSource;

    private void Start() {
        player = GameObject.Find("Player");
        string level = SceneManager.GetActiveScene().name;
        if (level.Equals("Menu")) {
            playingLevel = 0;
            pauseTab.SetActive(false);
        } else if (level.Contains("Level")) {
            int levelNumber;
            levelNumber = int.Parse(level.Substring(5));
            playingLevel = levelNumber;
        }
        musicSource.volume = VolumeSlider.currentVolume / 7f;
        musicSource.loop = true;
        musicSource.Play();
        retryCircle.enabled = false;
    }

    private void Update() {
        PressRetry();
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            LoadLevel(1);
        }
        if(musicSource.volume != VolumeSlider.currentVolume / 7f) {
            musicSource.volume = VolumeSlider.currentVolume / 7f;
        }
        if (Time.timeScale != 1) {
            musicSource.pitch = Mathf.Lerp(0.3f, 1, Time.timeScale);
        } else {
            musicSource.pitch = 1;
        }
    }

    public static string GetSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    public static bool InLevel() {
        return SceneManager.GetActiveScene().name.Contains("Level") || SceneManager.GetActiveScene().name.Contains("Tutorial");
    }

    public static void LoadTestScene() {
        SceneManager.LoadScene("TestScene");
    }

    public static void LoadLevel(int level) {
        if (level == -1) {
            playingLevel = -1;
            SceneManager.LoadScene("Tutorial");
        } else {
            playingLevel = level;
            SceneManager.LoadScene("Level" + level);
        }

    }

    public static void StaticRetry() {
        if (playingLevel>0) {
            SceneManager.LoadScene("Level" + playingLevel);
        } else if (playingLevel == -1) {
            SceneManager.LoadScene("Tutorial");
        }

    }

    public void Retry() {
        if (playingLevel == 0) {
            SceneManager.LoadScene("Menu");
        } else if (playingLevel == -1) {
            SceneManager.LoadScene("Tutorial");
        } else if (playingLevel > 0) {
            SceneManager.LoadScene("Level" + playingLevel);
        }

    }
    public void PressRetry() {
        if (tm.timePass != 0) {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                retryCircle.enabled = true;
            } else if (Input.GetKey(KeyCode.Tab)) {
                retryTime += Time.unscaledDeltaTime;
                retryCircle.fillAmount = retryTime / retryTimeRequired;
                if (retryTime >= retryTimeRequired) {
                    StaticRetry();
                }
            } else if (Input.GetKeyUp(KeyCode.Tab)) {
                retryCircle.enabled = false;
                retryTime = 0;
            }
        }
    }

    public static void FinishLevel() {
        //TODO
        SceneManager.LoadScene("Finish");
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
        playingLevel = 0;
    }

    public static void StaticBackToMenu() {
        SceneManager.LoadScene("Menu");
        playingLevel = 0;
    }

    public void Exit() {
        Application.Quit();
    }


}
