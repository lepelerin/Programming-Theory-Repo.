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
    private Dictionary<int, int> musicByLevel;
    private const string musicPath = "Text/MusicDico";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            musicSource = GetComponent<AudioSource>();
            /*string json = JsonConvert.SerializeObject(musicByLevel);
            Debug.Log(json);*/
            TextAsset textFile = (TextAsset)Resources.Load(musicPath);
            string json = textFile.text;
            musicByLevel = JsonConvert.DeserializeObject<Dictionary<int,int>>(json);

            DontDestroyOnLoad(gameObject);
            PlayMusic(0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(int level)
    {
        musicSource.Stop();
        musicSource.clip= musics[musicByLevel[level]];
        musicSource.Play();
    }

}
