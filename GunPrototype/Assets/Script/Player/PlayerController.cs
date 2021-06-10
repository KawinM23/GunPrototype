using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    static int hp;
    static int maxHp=100;

    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            SceneManager.LoadScene("TestScene");
        }
    }



    public static void getHit(int damage) {
        hp -= damage;
    }

    public float healthPercentage() {
        return ((float)hp / (float)maxHp);
    }
}
