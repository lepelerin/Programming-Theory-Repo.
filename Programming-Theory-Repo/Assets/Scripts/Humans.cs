using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humans : GeneralControl
{

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
        if (other.gameObject.CompareTag("Detector") && other.gameObject.GetComponentInParent<GeneralControl>().CompareTag("Player"))
        {

        }
    }

    public override void Scared(Transform transform)
    {
        base.Scared(transform);
    }
}
