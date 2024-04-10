using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans : GeneralControl
{
    private List<GameObject> dogs = new List<GameObject>();
    private AudioSource audioSourceHuman;
    // Start is called before the first frame update
    void Start()
    {
        controlRigidbody = GetComponent<Rigidbody>();
        audioSourceHuman = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
    public override void Scared(Vector3 position)
    {
        if(dogs.Count > 0)
        {
            AudioSource audioSource = dogs[0].GetComponent<AudioSource>();
            audioSource.Play();
            controlRigidbody.AddForce(-GetScaredDirection(dogs[0].transform.position) * forceMultiplicator, ForceMode.Impulse);
        }
        else
        {
            audioSourceHuman.Play();
            base.Scared(position);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
}
