using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;
    private void Awake()
    {
         if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        AddComponentToSoundArray(sounds);

    }
    private void Start()
    {
        PlaySound("Theme");
    }
    private void Update()
    {
        if(GameManager.Instance.State != GameState.Playing) StopSound("Theme");
    }



    private void AddComponentToSoundArray(Array arrayName)
    {
        foreach(Sound s in arrayName){
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }
    public void PlaySound(string audioName)
    {
        Sound s = Array.Find(sounds, s => s.Name == audioName);
        if(s == null) return;
        s.Source.Play();
    }
    public void StopSound(string audioName)
    {
        Sound s = Array.Find(sounds, s => s.Name == audioName);
        if(s == null) return;
        s.Source.Stop();
    }
}
