using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }
    void LateUpdate()
    {
        PositionCamera();
    }
    void PositionCamera()
    {
        playerCamera.transform.position = transform.position;
        playerCamera.transform.rotation = transform.rotation;
    }
    void MovePlayer()
    {
        float inputVertical=Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * inputVertical * speed);
        float inputHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right *  Time.deltaTime * inputHorizontal * speed);
    }
}
