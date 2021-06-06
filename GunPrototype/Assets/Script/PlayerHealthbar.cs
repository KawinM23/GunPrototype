using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    private Image Healthbar;

    // Start is called before the first frame update
    void Start()
    {
        Healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.fillAmount = pc.healthPercentage();
    }
}
