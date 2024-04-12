using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private Save player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            player = new Save();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerName(string name)
    {
        player.name = name;
    }
    public void LoadLevel(int level)
    {
        player.scene = level;
        SavePlayer();
        MusicManager.Instance.PlayMusic(level);
        SceneManager.LoadScene(level);
    }
    public bool LoadPlayer(int saveFile)
    {
        string path = Application.persistentDataPath + $"/savefile{saveFile}.json";
        if (File.Exists(path))
        {
            String json = File.ReadAllText(path);
            player = JsonUtility.FromJson<Save>(json);
            return true;
        }
        return false;
    }
    public void SavePlayer()
    {
        String json = JsonUtility.ToJson(player);
        File.WriteAllText(Application.persistentDataPath + $"/savefile{player.safeFile}.json", json);

    }
    public Save[] GetAllPlayer()
    {
        Save[] playerArray = new Save[3];
        for (int i = 0; i < playerArray.Length; i++)
        {
            if (LoadPlayer(i + 1))
                playerArray[i] = player;
            else
                playerArray[i] = new Save();
        }
        return playerArray;
    }
    public Save GetPlayer()
    {
        return player;
    }
    public void DeleteFile()
    {
        File.Delete(Application.persistentDataPath + $"/savefile{player.safeFile}.json");
    }
}