using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MusicManager Instance { get; private set; }
    private AudioSource musicSource;
    [SerializeField] AudioClip[] musics;
    private MusicByLevel[] musicByLevel;
    private const string musicPath = "Text/MusicDico";
    [SerializeField] TextAsset textFile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            musicSource = GetComponent<AudioSource>();
            string json = textFile.text;

            musicByLevel = JsonHelper.FromJson<MusicByLevel>(json);

            DontDestroyOnLoad(gameObject);
            PlayMusic(0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //ABSTRACTION
    public void PlayMusic(int level)
    {

        musicSource.Stop();
        musicSource.clip = musics[musicByLevel[level].song];
        musicSource.Play();

    }

}
