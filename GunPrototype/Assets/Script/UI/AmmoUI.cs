using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    private ShootController sc;

    [SerializeField] private Image image;

    public Color akColor;
    public Color smgColor;
    public Color sniperColor;

    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.Find("Player").GetComponent<ShootController>();

        if (LevelManager.InLevel()) {
            image.enabled = true;
        } else if (!LevelManager.InLevel()) {
            image.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sc.gunType == 0 && image.color != akColor) {
            image.color = akColor;
        }else if (sc.gunType == 1 && image.color != smgColor) {
            image.color = smgColor;
        }else if (sc.gunType == 2 && image.color != sniperColor) {
            image.color = sniperColor;
        }
        if (sc.reloadTime == 0){
            image.fillClockwise = false;
            image.fillAmount = sc.AmmoPercentage();
        } else {
            image.fillClockwise = true;
            image.fillAmount = sc.ReloadPercentage();
        }
        
    }
}
