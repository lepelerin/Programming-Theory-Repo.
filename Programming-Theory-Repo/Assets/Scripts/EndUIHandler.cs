using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUIHandler : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(0);
        MusicManager.Instance.PlayMusic(0);
    }
}
