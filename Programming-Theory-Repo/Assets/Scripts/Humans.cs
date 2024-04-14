using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Humans : GeneralControl
{
    [SerializeField] float speed;
    private AudioSource audioSourceHuman;
    private IEnumerator coroutine;
    [SerializeField] GameObject HumanMesh;
    private Animator animatorHuman;
    private bool isScared = false;
    private bool isNearDog = false;
    private bool isWallkingToDog = false;
    AudioSource dogAudioSource;
    Transform dogTransform;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceHuman = GetComponent<AudioSource>();
        animatorHuman = HumanMesh.GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isScared && !isNearDog)
        {
            animatorHuman.SetFloat("speed_f", speed);
            animatorHuman.Play("m_run");
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if(isWallkingToDog)
        {
            animatorHuman.SetFloat("speed_f", 0);
            transform.LookAt(dogTransform);
            transform.Translate(Vector3.forward * Time.deltaTime * speed/4);
        }
        else
        {
            animatorHuman.SetFloat("speed_f", 0);
        }
        CheckMovement();
    }

    void CheckMovement()
    {
        if(transform.position.y != 0) 
        {
            transform.position = new Vector3(transform.position.x,0,transform.position.z);
        }
    }

    void RotateWhenFaceWall(Collision collision)
    {
        transform.rotation = Quaternion.LookRotation(collision.GetContact(0).normal, transform.up);
        transform.SetPositionAndRotation(transform.position, new Quaternion(0, transform.rotation.y, 0, transform.rotation.w));
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dog"))
        {
            if(!isNearDog)
            {
                dogAudioSource = other.gameObject.transform.parent.gameObject.GetComponent<AudioSource>();
                dogAudioSource.Play();
            }
            isNearDog=true;
            dogTransform = other.gameObject.transform.parent.gameObject.transform;
            isWallkingToDog = true;
            StartCoroutine(WalkToDog());

        }
    }
    private IEnumerator WalkToDog()
    {
        yield return new WaitForSeconds(1);
        isWallkingToDog = false;
    }

    //POLYMORPHISM
    public override void Scared(Vector3 position)
    {
        if(coroutine!=null)
            StopCoroutine(coroutine);
        if (isNearDog)
        {
            dogAudioSource.Play();
        }
        else
        {
            audioSourceHuman.Play();
            RotateToward(position, true);
        }
        isScared = true;
        coroutine = ScaredNoMore();
        StartCoroutine(coroutine);
    }


    private IEnumerator ScaredNoMore()
    {
        yield return new WaitForSeconds(3);
        isScared = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            //Destroy(gameObject);
            Destroy(gameObject);
            SaveManager.Instance.LoadNextLevel();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            RotateWhenFaceWall(collision);
        }
    }
}
