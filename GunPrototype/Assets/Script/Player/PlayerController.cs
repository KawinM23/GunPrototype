using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private TimeManager tm;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private ParticleSystem diePs;
    // Start is called before the first frame update

    static int hp;
    static int maxHp = 100;
    private bool die;


    void Start()
    {
        tm = GameObject.Find("Manager").GetComponent<TimeManager>();
        hp = maxHp;
        die = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !die) {
            StartCoroutine(Die());
        }
    }

    public void SetFullHealth() {
        hp = maxHp;
    }

    IEnumerator Die() {
        die = true;
        sr.enabled = false;
        gameObject.GetComponent<PlayerMovement>().Die();
        diePs.Play();
        yield return StartCoroutine(tm.DoSlowmotionDie()); ;
        LevelManager.StaticRetry();
    }

    public static void getHit(int damage) {
        hp -= damage;
    }

    public float healthPercentage() => hp / (float)maxHp;
}
