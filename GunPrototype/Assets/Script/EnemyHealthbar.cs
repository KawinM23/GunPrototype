using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject[] healthGroups;

    public Color healthColor;
    public Color shieldColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGetHit() {
        float healthNumber = enemy.HealthPercentage() * healthGroups.Length;
        Debug.Log(healthNumber);
        for (float i = 0; i < healthGroups.Length; i++) {
            if (healthNumber > i) {
                healthGroups[(int)i].SetActive(true);
            } else {
                healthGroups[(int)i].SetActive(false);
            }
            if (enemy.isShield(i) && (enemy.shieldPointer == -1 || enemy.shieldPointer<i)) {
                Debug.Log("Shield "+ i+ enemy.shieldPointer + "Chnage Color");
                healthGroups[(int)i].GetComponent<SpriteRenderer>().color = healthColor;
            }
        }
    }


}
