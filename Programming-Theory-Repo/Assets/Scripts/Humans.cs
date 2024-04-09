using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans : GeneralControl
{
    private List<GameObject> dogs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        controlRigidbody = GetComponent<Rigidbody>();
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
            controlRigidbody.AddForce(-GetScaredDirection(dogs[0].transform.position) * forceMultiplicator, ForceMode.Impulse);
        }
        else
        {
            base.Scared(position);
        }
    }
}
