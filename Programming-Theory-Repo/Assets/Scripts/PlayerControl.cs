using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : GeneralControl
{
    [SerializeField] GameObject targetCamera;
    [SerializeField] float speed;
    [SerializeField] float rotaionSpeed;

    [SerializeField] GameObject ghost;
    private Animator animatorGhost;
    private AudioSource audioSourceGhost;
    // Start is called before the first frame update

    public delegate void Notify(Vector3 position);
    public event Notify ScaringHumans;
    private Vector3 scaredVector = Vector3.zero;
    private bool IsScared = false;
    private List<GameObject> cats = new List<GameObject>();

    private

    void Start()
    {
        targetCamera.transform.rotation = transform.rotation;
        animatorGhost = ghost.GetComponent<Animator>();
        audioSourceGhost = GetComponent<AudioSource>();
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
        ScaredMove();
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
    //ABSTRACTION
    void MovePlayer()
    {
        if (!IsScared)
        {
            float inputVertical = Input.GetAxis("Vertical");
            float inputHorizontal = Input.GetAxis("Horizontal");
            float angle = Mathf.Atan2(inputHorizontal, inputVertical) * Mathf.Rad2Deg;
            if (inputHorizontal != 0 || inputVertical != 0)
            {
                animatorGhost.SetBool("IsMoving", true);
                transform.rotation = targetCamera.transform.rotation;
            }
            else
            {
                animatorGhost.SetBool("IsMoving", false);
            }
            transform.Translate(Vector3.forward * Time.deltaTime * inputVertical * speed);
            transform.Translate(Vector3.right * Time.deltaTime * inputHorizontal * speed);
            transform.Rotate(Vector3.up * angle);

        }
    }
    void ScareHuman()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            audioSourceGhost.Play();
            if (cats.Count > 0)
            {
                Vector3 CatsVector = Vector3.zero;
                for (int i = 0; i < cats.Count; i++)
                {
                    AudioSource audioSource = cats[i].GetComponent<AudioSource>();
                    audioSource.Play();
                    CatsVector += cats[i].transform.position;
                }
                Scared(CatsVector);
            }
            else
            {
                animatorGhost.Play("attack_shift");
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

    public override void Scared(Vector3 position)
    {
        
        RotateToward(position, true);

        IsScared = true;
        animatorGhost.SetBool("IsMoving", true);
        StartCoroutine(ScaredNoMore());
    }
    IEnumerator ScaredNoMore()
    {
        yield return new WaitForSeconds(1);
        IsScared = false;
        animatorGhost.SetBool("IsMoving", false);
    }
    private void ScaredMove()
    {
        if (IsScared)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
