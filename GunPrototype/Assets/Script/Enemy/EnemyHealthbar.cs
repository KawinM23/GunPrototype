using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject[] healthGroups;
    [SerializeField] ParticleSystem ps;

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
        for (float i = 0; i < healthGroups.Length; i++) {
            if (healthNumber <= i && healthGroups[(int)i].activeSelf) {
                healthGroups[(int)i].SetActive(false);
            }
            if (enemy.isShield(i) && (enemy.shieldPointer == -1 || enemy.shieldPointer<i) && healthGroups[(int)i].GetComponent<SpriteRenderer>().color != healthColor) {
                Debug.Log("Shield "+ i + ""+ enemy.shieldPointer + "Change Color");
                healthGroups[(int)i].GetComponent<SpriteRenderer>().color = healthColor;
                ps.Play();
            }
        }
    }


}
