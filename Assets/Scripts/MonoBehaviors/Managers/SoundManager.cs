using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{

    public AudioSource player = default;


    public AudioClip[] Musics = default;
    public AudioClip[] Sounds = default;
    Dictionary<string, AudioClip> soundsDict = new Dictionary<string, AudioClip>();

    private void Start()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            soundsDict[Sounds[i].name] = Sounds[i];
        }
    }

    public float SoundVolum {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool soundMute {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public void PlayMusic(int index)
    {
        if (player.isPlaying)
            player.Stop();
        player.clip = Musics[index];
        player.Play();
    }

    public void StopMusic()
    {
        player.Stop();
    }

    public void playSound(string key)
    {
        player.PlayOneShot(soundsDict[ key]);
    }

}
