﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private GameObject player;
    private GameObject pauseTab;

    private static int playingLevel = 0;

    private void Start() {
        player = GameObject.Find("Player");
        pauseTab = GameObject.Find("PauseTab");
        string level = SceneManager.GetActiveScene().name;
        if (level.Equals("Menu")) {
            playingLevel = 0;
            pauseTab.SetActive(false);
        } else if (level.Contains("Level")) {
            int levelNumber;
            levelNumber =  int.Parse(level.Substring(5));
            playingLevel = levelNumber;
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Tab)) {
            Retry();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            LoadLevel(1);
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
        SceneManager.LoadScene("Level" + playingLevel);
    }

    public void Retry() {
        if(playingLevel == 0) {
            SceneManager.LoadScene("Menu");
        } else if (InLevel()) {
            SceneManager.LoadScene("Level" + playingLevel);
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


}
