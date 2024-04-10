using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Humans : GeneralControl
{
    [SerializeField] float speed;
    private List<GameObject> dogs = new List<GameObject>();
    private AudioSource audioSourceHuman;
    private bool isScared = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceHuman = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isScared)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
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
            dogs.Remove(other.gameObject.transform.parent.gameObject);
        }
    }

    //POLYMORPHISM
    public override void Scared(Vector3 position)
    {
        if(dogs.Count > 0)
        {
            AudioSource audioSource = dogs[0].GetComponent<AudioSource>();
            audioSource.Play();
            RotateToward(dogs[0].transform.position);
        }
        else
        {
            audioSourceHuman.Play();
            RotateToward(position, true);
        }
        isScared = true;
        StartCoroutine(ScaredNoMore());
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
