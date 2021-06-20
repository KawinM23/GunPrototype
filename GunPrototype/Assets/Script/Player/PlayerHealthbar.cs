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
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.InLevel() && !phb.activeSelf) {
            phb.SetActive(true);
        }else if (!LevelManager.InLevel() && phb.activeSelf) {
            phb.SetActive(false);
        }
        Healthbar.fillAmount = pc.healthPercentage();
    }
}
