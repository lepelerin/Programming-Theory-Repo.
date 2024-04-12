using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MusicManager Instance { get; private set; }
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            musicSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusic(AudioClip clip)
    {
        musicSource.clip=clip;
    }
    public void PlayMusic()
    {
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

}
