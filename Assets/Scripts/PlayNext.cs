using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNext : MonoBehaviour
{
    public HumanRandom humanRandom;
    public AudioSource audioSource;
    public void ChangeMusic()
    {
        audioSource.clip = humanRandom.PickSound().clip;
        audioSource.Play();
    }
}
