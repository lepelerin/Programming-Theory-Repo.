using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.WSA;
using static UnityEditor.PlayerSettings;

[CustomEditor(typeof(CatControl))]
public class CatControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    public void OnSceneGUI()
    {
        CatControl CatControl = (CatControl)target;
        Transform transform = CatControl.transform;
        
        for(int i = 0;i<CatControl.positionNode.Count;i++)
        {
            CatControl.positionNode[i]= Handles.PositionHandle(CatControl.positionNode[i], transform.rotation);
        }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawGizmo(CatControl catControl, GizmoType gizmoType)
    {
        for(int i = 0;i< catControl.positionNode.Count; i++)
        {
            Gizmos.DrawSphere(catControl.positionNode[i], 0.125f);
            if(i+1< catControl.positionNode.Count)
            {
                Handles.DrawDottedLine(catControl.positionNode[i], catControl.positionNode[i+1],3);
            }
            else
            {
                Handles.DrawDottedLine(catControl.positionNode[i], catControl.positionNode[0], 3);
            }
        }
    }

}
