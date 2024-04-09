using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : GeneralControl
{
    [SerializeField] GameObject targetCamera;
    [SerializeField] float speed;
    [SerializeField] float rotaionSpeed;
    // Start is called before the first frame update

    public delegate void Notify(Transform transform);
    public event Notify ScaringHumans;


    void Start()
    {
        targetCamera.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
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
            ScaringHumans?.Invoke(transform);
        }
    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Detector") && other.gameObject.GetComponentInParent<GeneralControl>().CompareTag("Human"))
        {
            ScaringHumans += other.gameObject.GetComponentInParent<Humans>().Scared;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Detector") && other.gameObject.GetComponentInParent<GeneralControl>().CompareTag("Human"))
        {
            ScaringHumans -= other.gameObject.GetComponentInParent<Humans>().Scared;
        }
    }
}
