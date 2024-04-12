using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject saveScreen;
    [SerializeField] GameObject eventSystemObject;
    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem=eventSystemObject.GetComponent<EventSystem>();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        saveScreen.SetActive(true);
        eventSystem.SetSelectedGameObject( GameObject.Find("Save 1 Button"));
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
        saveScreen.SetActive(false);
        eventSystem.SetSelectedGameObject(GameObject.Find("Start Button"));
    }
}
