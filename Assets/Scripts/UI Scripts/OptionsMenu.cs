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
    public string slideName;

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        audiomixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
