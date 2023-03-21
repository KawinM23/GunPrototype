using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    private PlayerController pc;
    private Image Healthbar;
    private GameObject phb;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        Healthbar = GetComponent<Image>();
        phb = GameObject.Find("PlayerHealthbar");

        if (LevelManager.InLevel()) {
            phb.SetActive(true);
        } else if (!LevelManager.InLevel()) {
            phb.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.fillAmount = pc.healthPercentage();
    }
}
