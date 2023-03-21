using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private TimeManager tm;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private ParticleSystem diePs;

    [Header("Sounds")]
    [SerializeField] AudioSource healthSource;
    [SerializeField] AudioClip lossHealth;

    static int hp;
    static int maxHp = 100;
    private bool die;

    public static PlayerController Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
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

    public void getHit(int damage) {
        hp -= damage;
        PlayLossHealthSound(damage);
    }

    public float healthPercentage() => hp / (float)maxHp;

    void PlayLossHealthSound(int damage){
        if(damage > 0){
            healthSource.PlayOneShot(lossHealth,.8f);
        }
    }
}
