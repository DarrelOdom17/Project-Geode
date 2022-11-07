using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    private GameObject audioManager;
    private void Start()
    {
        audioManager = GameObject.Find("AudioManager");
    }
    public AudioMixer audiomixer;

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
