using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralControl : MonoBehaviour
{
    [SerializeField] float detectRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetectOther();
    }

    protected void DetectOther()
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position, detectRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(colliders[i].gameObject.transform.name);
        }
    }
}
