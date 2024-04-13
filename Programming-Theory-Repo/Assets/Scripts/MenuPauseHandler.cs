using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPauseHandler : MonoBehaviour
{
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject eventSystemObject;
    private EventSystem eventSystem;
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } }
    private void Start()
    {
        eventSystem = eventSystemObject.GetComponent<EventSystem>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
            if (PauseScreen.activeSelf)
                Resume();
            else
                PauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(GameObject.Find("Continue Button"));
        isPaused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        isPaused = false;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
