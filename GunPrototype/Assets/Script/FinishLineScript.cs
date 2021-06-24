using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineScript : MonoBehaviour
{
    private TimeManager tm;
    public float timeLimited;

    [SerializeField] private List<GameObject> enemies;

    private void Start() {
        tm = GameObject.Find("Manager").GetComponent<TimeManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision != null & collision.CompareTag("Player")) {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy")) {
                enemies.Add(go);
            }
            FinishManager.FinishData(SceneManager.GetActiveScene().name,tm.timePass, timeLimited, enemies.Count == 0);
            LevelManager.FinishLevel();
        }
    }
}
