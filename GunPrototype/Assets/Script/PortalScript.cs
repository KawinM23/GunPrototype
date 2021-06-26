using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private SaveSystem ss;

    [SerializeField] private SpriteRenderer star1Sprite;
    [SerializeField] private SpriteRenderer star2Sprite;
    [SerializeField] private SpriteRenderer star3Sprite;

    public Color star1Color;
    public Color star2Color;
    public Color star3Color;



    private bool onPortal;

    public int level;

    private void Start() {
        foreach(LevelData ld in ss.dataList) {
            if (ld.levelName.Equals("Level" + level)) {
                if (!ld.completed) {
                    star1Sprite.enabled = false;
                    star2Sprite.enabled = false;
                    star3Sprite.enabled = false;
                }
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
        }
    }

    private void Update() {
        if (onPortal && Input.GetKeyDown(KeyCode.Mouse1)) {
            LevelManager.LoadLevel(level);
        }
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
