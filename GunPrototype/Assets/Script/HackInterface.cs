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
    [SerializeField] private PressParticle pressParticle;

    // Start is called before the first frame update
    void Start()
    {
        hi = GameObject.Find("HackInterface");
        canvasTransform = GameObject.Find("Canvas").transform;
        hi.SetActive(false);
        hc = GameObject.Find("Player").GetComponent<HackController>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ShowHackList(KeyCode[] keyCodes) {
        Debug.Log("ShowHackList");
        for(int i = 0; i < keyCodes.Length; i++) {
            GameObject tp = Instantiate(template);
            tp.transform.SetParent(hi.transform);
        }

        for (int i = 0; i < keyCodes.Length; i++) {
            if (keyCodes[i] == KeyCode.W) {
                hi.transform.GetChild(i).GetComponent<Image>().sprite = upImage;
            } else if(keyCodes[i] == KeyCode.A) {
                hi.transform.GetChild(i).GetComponent<Image>().sprite = leftImage;
            } else if (keyCodes[i] == KeyCode.S) {
                hi.transform.GetChild(i).GetComponent<Image>().sprite = rightImage;
            } else if (keyCodes[i] == KeyCode.D) {
                hi.transform.GetChild(i).GetComponent<Image>().sprite = downImage;
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
        Debug.Log(canvasTransform.InverseTransformPoint(hi.transform.GetChild(index).position));
        pressParticle.Press(canvasTransform.InverseTransformPoint(hi.transform.GetChild(index).position));
    }
}
