using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private AudioClip catchSound, failSound, laserSound, flashSound, splashSound, mouseTrapSound;
    public AudioSource soundEffectSource, mouseSoundSource;

    [Serializable]
    public struct Clip {
        public string name;
        public AudioClip audio;
        public int priority;

        public Clip(string n, AudioClip a, int p)
        {
            this.name = n;
            this.audio = a;
            this.priority = p;
        }
    }
    
    public struct Source
    {
        public string name;
        public AudioSource source;
        public Source(string n, AudioSource a)
        {
            this.name = n;
            this.source = a;
        }
    }

    public List<Clip> clips = new List<Clip>();
    public List<Source> sources = new List<Source>();
    void Start()
    {
        sources.Add(new Source("soundEffectSource", soundEffectSource));
        sources.Add(new Source("mouseSoundSource", mouseSoundSource));
        clips.Add(new Clip("catchSound", catchSound, 200));
        clips.Add(new Clip("failSound", failSound, 201));
        clips.Add(new Clip("laserSound", laserSound, 100));
        clips.Add(new Clip("flashSound", flashSound, 110));
        clips.Add(new Clip("splashSound", splashSound, 110));
        clips.Add(new Clip("mouseTrapSound", mouseTrapSound, 111));
    }

    public void PlaySound(string audioClip)
    {
        var currentClip = clips.Find(c => c.name == audioClip);
        soundEffectSource.clip = currentClip.audio;
        soundEffectSource.Play();
    }

    public void MuteMice() {  
        mouseSoundSource.mute = true;  
    }
    public void ResumeMice() {  mouseSoundSource.mute = false;  }

}
