using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    public AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        Audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float vol) {
        audioMixer.SetFloat("vol", slider.value);
    }

}
