using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PositionCamera();
    }

    void PositionCamera()
    {
        playerCamera.transform.position = transform.position;
        playerCamera.transform.rotation = transform.rotation;
    }
}
