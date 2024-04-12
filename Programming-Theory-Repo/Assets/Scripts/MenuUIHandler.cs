using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
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
    [SerializeField] GameObject newSaveScreen;
    [SerializeField] GameObject eventSystemObject;
    [SerializeField] TextMeshProUGUI[] buttonSaveText;
    [SerializeField] GameObject saveOption;
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI playerNameField;
    private EventSystem eventSystem;
    private Save[] playerArray;
    private void Start()
    {
        eventSystem=eventSystemObject.GetComponent<EventSystem>();
        playerArray = SaveManager.Instance.GetAllPlayer();
        for (int i = 0; i < playerArray.Length; i++)
        {
            if (!string.IsNullOrEmpty(playerArray[i].name))
                buttonSaveText[i].text = playerArray[i].name;
        }
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        saveScreen.SetActive(true);
        saveOption.SetActive(false);
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
        if (SaveManager.Instance.LoadPlayer(path))
        {
            
            saveOption.SetActive(true);
            saveScreen.SetActive(false);
            playerName.text= SaveManager.Instance.GetPlayer().name;
            eventSystem.SetSelectedGameObject(GameObject.Find("Start With This Save"));
        }
        else
        {
            SaveManager.Instance.SetPlayerSaveFile(path);
            newSaveScreen.SetActive(true);
            saveScreen.SetActive(false);
            eventSystem.SetSelectedGameObject(GameObject.Find("NameField"));
        }
    }

    public void Back()
    {
        startScreen.SetActive(true);
        saveScreen.SetActive(false);
        eventSystem.SetSelectedGameObject(GameObject.Find("Start Button"));
    }
    public void PlayGame()
    {

        SaveManager.Instance.LoadLevel(SaveManager.Instance.GetPlayer().scene);
    }
    public void PlayNewGame()
    {
        Debug.Log(playerNameField.text);
        SaveManager.Instance.SetPlayerName(playerNameField.text);
        SaveManager.Instance.LoadLevel(1);
    }
    public void DeleteFile()
    {

        SaveManager.Instance.DeleteFile();
        playerArray = SaveManager.Instance.GetAllPlayer();
        for (int i = 0; i < playerArray.Length; i++)
        {
            if (string.IsNullOrEmpty(playerArray[i].name))
                buttonSaveText[i].text = $"Save {i+1}";
        }
        StartGame();
    }
}
