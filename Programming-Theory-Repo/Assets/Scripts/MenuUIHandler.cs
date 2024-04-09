using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
