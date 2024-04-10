using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : GeneralControl
{
    [SerializeField] GameObject targetCamera;
    [SerializeField] float speed;
    [SerializeField] float rotaionSpeed;

    [SerializeField] GameObject ghost;
    private Animator animatorGhost;

    // Start is called before the first frame update

    public delegate void Notify(Vector3 position);
    public event Notify ScaringHumans;

    private List<GameObject> cats = new List<GameObject>();

    private

    void Start()
    {
        controlRigidbody = GetComponent<Rigidbody>();
        targetCamera.transform.rotation = transform.rotation;
        animatorGhost = ghost.GetComponent<Animator>();
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
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");
        float angle = Mathf.Atan2(inputHorizontal, inputVertical) * Mathf.Rad2Deg;
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            transform.rotation = targetCamera.transform.rotation;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * inputVertical * speed);
        transform.Translate(Vector3.right * Time.deltaTime * inputHorizontal * speed);
        transform.Rotate(Vector3.up * angle);


    }
    void ScareHuman()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            animatorGhost.Play("attack");
            Debug.Log("attack");
            if (cats.Count > 0)
            {
                Vector3 CatsVector = Vector3.zero;
                for (int i = 0; i < cats.Count; i++)
                {
                    Debug.Log(cats[i].transform.position);
                    CatsVector += cats[i].transform.position;
                }
                Debug.Log(CatsVector);
                Scared(CatsVector);
            }
            else
            {
                animatorGhost.Play("attack");
                ScaringHumans?.Invoke(transform.position);
            }
        }
    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            ScaringHumans += other.gameObject.GetComponentInParent<Humans>().Scared;
        }
        if (other.gameObject.CompareTag("Cat"))
        {
            cats.Add(other.gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            ScaringHumans -= other.gameObject.GetComponentInParent<Humans>().Scared;
        }
        if (other.gameObject.CompareTag("Cat"))
        {
            cats.Remove(other.gameObject.transform.parent.gameObject);
        }
    }
}
