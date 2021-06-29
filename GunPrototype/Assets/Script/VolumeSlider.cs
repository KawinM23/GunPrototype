using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [HideInInspector] public static float currentVolume = 0.25f;
    private AudioSource shootSource;

    // Start is called before the first frame update
    void Start()
    {
        shootSource = GameObject.Find("ShootSource").GetComponent<AudioSource>();

        volumeSlider.value = currentVolume;
        shootSource.volume = currentVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if(volumeSlider.value != currentVolume) {
            currentVolume = volumeSlider.value;
            shootSource.volume = currentVolume;
        }
        
    }
}
