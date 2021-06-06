using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject[] healthGroups;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float healthNumber = enemy.HealthPercentage() * healthGroups.Length;
        for (float i =0; i< healthGroups.Length; i++) {
            if (healthNumber > i) {
                healthGroups[(int)i].SetActive(true);
            } else {
                healthGroups[(int)i].SetActive(false);
            }
        }
    }


}
