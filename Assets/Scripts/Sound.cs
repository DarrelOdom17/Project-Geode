using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Forces unity to show us this in the inspector.
public class Sound
{
public string name;
public AudioClip clip;

[Range(0f, 1f)]
public float volume = 1; //This float will now only operate between 0 and 1
[Range(.1f, 3f)]
public float pitch = 1;

public float spatialBlend = 1;

public float maxDistance;

public bool playOnAwake;


public AudioSource source;
}