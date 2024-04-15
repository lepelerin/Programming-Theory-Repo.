using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InfoPoint : MonoBehaviour
{
    [SerializeField] GameObject Mesh;
    [SerializeField] float hightMax;
    [SerializeField] float hightMin;
    [SerializeField] float hightSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField][TextArea(15, 20)] string info;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject Detector;
    private bool isMovingUp;
    // Start is called before the first frame update
    void Start()
    {
        text.text = info;
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        AnimateMesh();
    }
    public void AnimateMesh()
    {
        if (Mesh.transform.position.y < hightMin)
        {
            isMovingUp = true;
        }
        if (Mesh.transform.position.y > hightMax)
        {
            isMovingUp = false;
        }
        if (isMovingUp)
        {
            Mesh.transform.Translate(Vector3.up * hightSpeed * Time.deltaTime);
        }
        else
        {
            Mesh.transform.Translate(Vector3.down * hightSpeed * Time.deltaTime);
        }
        Mesh.transform.Rotate(Vector3.up *  rotationSpeed * Time.deltaTime);

    }

    public void ShowCanvas()
    {
        Canvas.SetActive(true);
    }
    public void HideCanvas()
    {
        Canvas.SetActive(false);
    }
}
