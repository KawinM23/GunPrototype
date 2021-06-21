using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackInterface : MonoBehaviour
{
    GameObject hi;
    HackController hc;
    Transform canvasTransform;

    [SerializeField] private GameObject template;
    [SerializeField] private Sprite upImage;
    [SerializeField] private Sprite downImage;
    [SerializeField] private Sprite leftImage;
    [SerializeField] private Sprite rightImage;

    [SerializeField] private GameObject upParticle;
    [SerializeField] private GameObject downParticle;
    [SerializeField] private GameObject leftParticle;
    [SerializeField] private GameObject rightParticle;

    float size;

    private void Awake() {
        hi = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        hc = GameObject.Find("Player").GetComponent<HackController>();
    }

    public void ShowHackList(KeyCode[] keyCodes) {
        size = Mathf.Clamp((Screen.height / 5), 80, 200);
        for (int i = 0; i < keyCodes.Length; i++) {
            GameObject tp = Instantiate(template, hi.transform);
            tp.GetComponent<RectTransform>().sizeDelta = new Vector2(size,size);
        }

        for (int i = 0; i < keyCodes.Length; i++) {
            if (keyCodes[i] == KeyCode.W) {
                hi.transform.GetChild(i).GetComponent<Image>().sprite = upImage;
            } else if(keyCodes[i] == KeyCode.A) {
                hi.transform.GetChild(i).GetComponent<Image>().sprite = leftImage;
            } else if (keyCodes[i] == KeyCode.S) {
                hi.transform.GetChild(i).GetComponent<Image>().sprite = downImage;
            } else if (keyCodes[i] == KeyCode.D) {
                hi.transform.GetChild(i).GetComponent<Image>().sprite = rightImage;
            }
        }

        hi.SetActive(true);
    }

    public void ShowInterface() {
        hi.SetActive(true);
    }

    public void HideInterface() {
        hi.SetActive(false);
        foreach (Transform child in hi.transform) {
            Destroy(child.gameObject);
        }
    }

    public void Press(int index) {
        GameObject newParticle = Instantiate(GetParticle(index), canvasTransform);
        newParticle.GetComponent<RectTransform>().position = hi.transform.GetChild(index).gameObject.GetComponent<RectTransform>().position;
        newParticle.GetComponent<ParticleSystem>().Play();
        Destroy(newParticle, 1f);
    }

    public GameObject GetParticle(int index) {
        switch (hc.GetKeyNumber(index)) {
            case 1:
                return upParticle;
            case 2:
                return leftParticle;
            case 3:
                return downParticle;
            case 4:
                return rightParticle;
            default:
                return upParticle;
        }
    }

}
