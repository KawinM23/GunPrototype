using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private GameObject player;

    private static int playingLevel = 0;

    private void Start() {
        player = GameObject.Find("Player");
        string level = SceneManager.GetActiveScene().name;
        if (level.Equals("Menu")) {
            playingLevel = 0;
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

    public static void LoadTestScene() {
        SceneManager.LoadScene("TestScene");
    }

    public static void LoadLevel(int level) {
        SceneManager.LoadScene("Level"+ level);
        playingLevel = level;
    }

    public static void Retry() {
        SceneManager.LoadScene("Level" + playingLevel);
    }

    public static void FinishLevel() {
        //TODO
        SceneManager.LoadScene("Menu");
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
        playingLevel = 0;
    }
}
