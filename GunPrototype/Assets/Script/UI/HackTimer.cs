using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackTimer : MonoBehaviour
{
    GameObject ht;
    HackController hc;
    Image image1;
    Image image2;
    public Color maxColor;
    public Color minColor;

    float percentage;
    Color color;

    private void Awake() {
        ht = this.gameObject;
    }

    void Start()
    {
        hc = GameObject.Find("Player").GetComponent<HackController>();
        image1 = GameObject.Find("Timebar1").GetComponent<Image>();
        image2 = GameObject.Find("Timebar2").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hc.isHacking) {
            percentage = hc.GetTimePercentage();
            color = Color.Lerp(minColor, maxColor, percentage);
            image1.fillAmount = percentage;
            image2.fillAmount = percentage;
            image1.color = color;
            image2.color = color;
        }
    }

    public void ShowTimer() {
        ht.SetActive(true);
    }

    public void HideTimer() {
        ht.SetActive(false);
    }
}
