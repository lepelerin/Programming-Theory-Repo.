using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans : GeneralControl
{
    [SerializeField] float speed;
    private List<GameObject> dogs = new List<GameObject>();
    private AudioSource audioSourceHuman;
    private bool isScared = false;
    private bool isScaredDog = false;
    private Vector3 ghostPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceHuman = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isScaredDog)
        {
            transform.Translate(-GetScaredDirection(dogs[0].transform.position) * Time.deltaTime * speed);
        }
        if (isScared)
        {
            transform.Translate(GetScaredDirection(ghostPosition) * Time.deltaTime * speed);
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dog"))
        {

            dogs.Add(other.gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Dog"))
        {
            dogs.Add(other.gameObject.transform.parent.gameObject);
        }
    }

    //POLYMORPHISM
    public override void Scared(Vector3 position)
    {
        if(dogs.Count > 0)
        {
            AudioSource audioSource = dogs[0].GetComponent<AudioSource>();
            audioSource.Play();
            isScaredDog = true;
            StartCoroutine(ScaredDogNoMore());
        }
        else
        {
            ghostPosition = position;
            audioSourceHuman.Play();
            isScared = true;
            StartCoroutine(ScaredNoMore());
        }
    }
    private IEnumerator ScaredDogNoMore()
    {
        yield return new WaitForSeconds(3);
        isScaredDog = false;
    }
    private IEnumerator ScaredNoMore()
    {
        yield return new WaitForSeconds(3);
        isScared = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
}
