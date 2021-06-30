using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLineScript : MonoBehaviour
{
    private TimeManager tm;
    public float timeLimited;

    private List<GameObject> enemies;

    private void Start() {
        tm = GameObject.Find("Manager").GetComponent<TimeManager>();
        Text timeRequired = GameObject.Find("TimeRequired").GetComponent<Text>();
        if (SceneManager.GetActiveScene().name.Contains("Level")) {
            timeRequired.text = "/ " + timeLimited.ToString();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision != null & collision.CompareTag("Player")) {
            if (SceneManager.GetActiveScene().name.Contains("Level")) {
                enemies = new List<GameObject>();
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy")) {
                    enemies.Add(go);
                }
                FinishManager.FinishData(SceneManager.GetActiveScene().name, tm.timePass, timeLimited, enemies.Count == 0);
                LevelManager.FinishLevel();
            }else if (SceneManager.GetActiveScene().name.Contains("Tutorial")) {
                LevelManager.StaticBackToMenu();
            }
        }
    }
}
