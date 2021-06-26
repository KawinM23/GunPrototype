using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDoor : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies;

    private Vector3 reduceVector;

    private void Start() {
        reduceVector = new Vector3(transform.localScale.x, 0, 0);
    }

    private void Update() {
        enemies.RemoveAll(item => item == null);
        if (enemies.Count == 0 && transform.localScale.x!=0) {

            transform.localScale -= reduceVector/0.75f * Time.deltaTime;
            if(transform.localScale.x <= 0) {
                Destroy(gameObject);
            }
        }
    }

}
