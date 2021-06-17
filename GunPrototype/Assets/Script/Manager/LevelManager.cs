using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private static int playingLevel = 0;


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
}
