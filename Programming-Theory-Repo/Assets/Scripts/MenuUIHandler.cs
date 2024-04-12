using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject SaveScreen;


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

    public void Save(int path)
    {
        if (!SaveManager.Instance.LoadPlayer(path))
        {
            //create player
            SaveManager.Instance.LoadLevel(1);
        }
        else
            SaveManager.Instance.LoadLevel(1);
    }

    public void Back()
    {
        startScreen.SetActive(true);
        SaveScreen.SetActive(false);
    }
}
