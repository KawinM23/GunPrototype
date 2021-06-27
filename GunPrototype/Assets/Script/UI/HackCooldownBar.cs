using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackCooldownBar : MonoBehaviour
{
    private HackController hc;
    [SerializeField] private Image image;

    private void Start() {
        hc = GameObject.Find("Player").GetComponent<HackController>();
        if (LevelManager.InLevel()) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (hc.GetCooldownPercentage() != 1) {
            image.fillAmount = hc.GetCooldownPercentage();
        } else {
            image.fillAmount = 1f;
        }
    }
}
