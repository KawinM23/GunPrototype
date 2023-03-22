using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    private SaveSystem ss;

    private SpriteRenderer star1Sprite;
    private SpriteRenderer star2Sprite;
    private SpriteRenderer star3Sprite;

    public Color star1Color;
    public Color star2Color;
    public Color star3Color;

    private bool onPortal;

    public int level;

    private void Start() {
        StartCoroutine(LoadData());
        
    }

    private void Update() {
        if (onPortal && Input.GetKeyDown(KeyCode.Mouse1)) {
            LevelManager.LoadLevel(level);
        }
    }

    IEnumerator LoadData() {
        if (level != -1) {
            ss = GameObject.Find("SaveSystem").GetComponent<SaveSystem>();
            yield return new WaitUntil(() => ss.dataList.Count == ss.levelNumber);
        
            star1Sprite = transform.Find("Star1").GetComponent<SpriteRenderer>();
            star2Sprite = transform.Find("Star2").GetComponent<SpriteRenderer>();
            star3Sprite = transform.Find("Star3").GetComponent<SpriteRenderer>();

            foreach (LevelData ld in ss.dataList) {
                if (ld.levelName.Equals("Level" + level)) {
                    if (!ld.completed) {
                        star1Sprite.enabled = false;
                        star2Sprite.enabled = false;
                        star3Sprite.enabled = false;
                    } else {
                        if (ld.stars[0]) {
                            star1Sprite.color = star1Color;
                        }
                        if (ld.stars[1]) {
                            star2Sprite.color = star2Color;
                        }
                        if (ld.stars[2]) {
                            star3Sprite.color = star3Color;
                        }
                    }
                }else{
                    star1Sprite.enabled = false;
                    star2Sprite.enabled = false;
                    star3Sprite.enabled = false;
                }
            }
        }
        yield return null;

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            onPortal = true;
            ps.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            onPortal = false;
            ps.Stop();
        }
    }
}
