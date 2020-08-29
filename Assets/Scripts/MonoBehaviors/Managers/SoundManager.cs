using System;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{

    public AudioSource player = default;


    public AudioClip[] Musics = default;
    public AudioClip[] Sounds = default;
    public string[] Keys = default;

    private void Start()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Keys[i] = Sounds[i].name;
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

    public void playSound(string key)
    {
        player.PlayOneShot(Sounds[Array.IndexOf(Keys, key)]);
    }

}
