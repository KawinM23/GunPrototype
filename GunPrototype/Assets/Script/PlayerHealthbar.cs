using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    private PlayerController pc;
    private Image Healthbar;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        Healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.fillAmount = pc.healthPercentage();
    }
}
