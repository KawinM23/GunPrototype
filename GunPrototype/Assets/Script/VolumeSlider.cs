using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [HideInInspector] public static float currentVolume;
    private AudioSource shootSource;
    private AudioSource hackSource;

    void Awake()
    {
        shootSource = GameObject.Find("ShootSource").GetComponent<AudioSource>();
        hackSource = GameObject.Find("HackSource").GetComponent<AudioSource>();

        currentVolume = volumeSlider.value;
        shootSource.volume = currentVolume / 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentVolume != volumeSlider.value) {
            currentVolume = volumeSlider.value;
        }
        if(shootSource.volume != currentVolume / 5f){
            shootSource.volume = currentVolume / 5f;
        }
        if(hackSource.volume != currentVolume / 5f){
            hackSource.volume = currentVolume / 5f;
        }
    }
}
