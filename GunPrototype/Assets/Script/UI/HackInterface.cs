﻿using System.Collections;
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
    List<GameObject> hackObjects;
    public Color grey;

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
        hackObjects = new List<GameObject>();
        size = Mathf.Clamp((Screen.height / 5), 80, 100);
        for (int i = 0; i < keyCodes.Length; i++) {
            GameObject tp = Instantiate(template, hi.transform);
            tp.GetComponent<RectTransform>().sizeDelta = new Vector2(size,size);
            hackObjects.Add(tp);
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
        hackObjects[index].GetComponent<Image>().color = grey;
        GameObject newParticle = Instantiate(GetParticle(index), canvasTransform);
        newParticle.GetComponent<RectTransform>().position = hi.transform.GetChild(index).gameObject.GetComponent<RectTransform>().position;
        newParticle.GetComponent<ParticleSystem>().Play();
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
