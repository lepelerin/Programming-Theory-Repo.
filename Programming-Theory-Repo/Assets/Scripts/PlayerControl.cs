using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : GeneralControl
{
    [SerializeField] GameObject targetCamera;
    [SerializeField] float speed;
    [SerializeField] float rotaionSpeed;
    // Start is called before the first frame update
    void Start()
    {
        targetCamera.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        DetectOther();
        ScareHuman();
    }
    void FixedUpdate()
    {
        RotateCamera();
        MovePlayer();
    }
    void LateUpdate()
    {
        PositionCamera();
    }
    void PositionCamera()
    {
        targetCamera.transform.position = transform.position;
    }
    void RotateCamera()
    {
        float inputRotational = Input.GetAxis("Rotational");
        targetCamera.transform.Rotate(Vector3.up * Time.deltaTime * inputRotational * rotaionSpeed);
    }
    void MovePlayer()
    {
        float inputVertical=Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        if (inputHorizontal != 0 || inputVertical != 0)
        {
            transform.rotation = targetCamera.transform.rotation;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * inputVertical * speed);
        transform.Translate(Vector3.right *  Time.deltaTime * inputHorizontal * speed);

    }
    void ScareHuman()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Boo");
        }
    }
}
