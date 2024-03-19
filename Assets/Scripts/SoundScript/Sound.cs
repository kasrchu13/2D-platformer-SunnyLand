using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public string Name;
    public AudioClip Clip;
    [HideInInspector]
    public AudioSource Source;
    [Range(0,1)]
    public float Volume;
    [Range(.1f,3)]
    public float Pitch;
    public bool Loop;
}
