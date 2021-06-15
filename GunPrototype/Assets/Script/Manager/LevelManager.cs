using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static int playingLevel;

    private void Awake() {
        playingLevel = 0;
    }

    public static void LoadTestScene() {
        SceneManager.LoadScene("TestScene");
    }

    public static void LoadScene(int sceneIndex) {
        SceneManager.LoadScene("Level"+sceneIndex);
    }

    public static void Retry() {
        SceneManager.LoadScene("Level" + playingLevel);
    }
}
