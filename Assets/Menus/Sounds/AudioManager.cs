using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    public String playOnAwake;
    public bool destroy = true;

    public static AudioManager instance;
    private void Awake()
    {
        if (destroy)
            Destroy(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play(playOnAwake);
    }
    public void Play(string name)
    {
        Sounds s =Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
            return;
        s.source.Play();
        
    }
}
