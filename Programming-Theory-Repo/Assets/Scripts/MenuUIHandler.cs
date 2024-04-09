using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    public void Save(string path)
    {

    }

    public void Back()
    {
        startScreen.SetActive(true);
        SaveScreen.SetActive(false);
    }
}
