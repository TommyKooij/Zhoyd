using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private int worldMusicNumber, bossMusicNumber;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource mainMenu;
    public AudioSource[] worldMusic;
    public AudioSource[] bossMusic;
    public AudioSource[] sfx;

    public void PlayMainMenuMusic()
    {
        for (int i = 0; i < worldMusic.Length; i++)
        {
            worldMusic[i].Stop();
        }

        mainMenu.Play();
    }

    public void PlayWorldMusic(int worldMusicNumber)
    {
        if(!worldMusic[worldMusicNumber].isPlaying)
        {
            mainMenu.Stop();

            worldMusic[worldMusicNumber].Play();
        }
    }

    public void PlayBossMusic(int bossMusicNumber)
    {
        for (int i = 0; i < worldMusic.Length; i++)
        {
            worldMusic[i].Stop();
        }

        bossMusic[bossMusicNumber].Play();
    }

    public void ForceStopMusic()
    {
        for (int i = 0; i < worldMusic.Length; i++)
        {
            worldMusic[i].Stop();
        }
    }
}
