using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    private ShootController sc;

    [SerializeField] private Image image;
    [SerializeField] private Text ammoText;

    public Color akColor;
    public Color smgColor;
    public Color sniperColor;
    public Color white;

    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.Find("Player").GetComponent<ShootController>();

        if (LevelManager.InLevel()) {
            image.enabled = true;
        } else {
            image.enabled = false;
            ammoText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sc.gunType == 0 && image.color != akColor) {
            image.color = akColor;
            ammoText.color = akColor + white;
        } else if (sc.gunType == 1 && image.color != smgColor) {
            image.color = smgColor;
            ammoText.color = smgColor + white;
        } else if (sc.gunType == 2 && image.color != sniperColor) {
            image.color = sniperColor;
            ammoText.color = sniperColor + white;
        }
        if (sc.reloadTime == 0){
            image.fillClockwise = false;
            image.fillAmount = sc.AmmoPercentage();
        } else {
            image.fillClockwise = true;
            image.fillAmount = sc.ReloadPercentage();
        }
        ammoText.text = sc.GetAmmoText();
        
        
    }
}
