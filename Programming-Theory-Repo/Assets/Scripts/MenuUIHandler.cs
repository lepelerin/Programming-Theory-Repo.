using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject SaveScreen;
    [SerializeField] AudioClip StartMusic;

    private void Awake()
    {
        MusicManager.Instance.SetMusic(StartMusic);
        MusicManager.Instance.PlayMusic();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        SaveScreen.SetActive(true);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void Save(string path)
    {
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        startScreen.SetActive(true);
        SaveScreen.SetActive(false);
    }
}
