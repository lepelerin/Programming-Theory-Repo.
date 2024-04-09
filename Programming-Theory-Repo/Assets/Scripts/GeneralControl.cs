using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class GeneralControl : MonoBehaviour
{
    protected List<GameObject> gameObjectsInRage = new List<GameObject>();
    protected Rigidbody controlRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        controlRigidbody= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Detector"))
        {
            gameObjectsInRage.Add(other.gameObject);
            Debug.Log(gameObject.name+ " / " +other.gameObject.name +" / "+ other.gameObject.GetComponentInParent<GeneralControl>().name);
        }
    }
    public virtual void Scared(Transform transform)
    {
        controlRigidbody.AddForce(GetScaredDirection(transform),ForceMode.Impulse);
    }
    protected Vector3 GetScaredDirection(Transform transform)
    {
        return (this.transform.position - transform.position).normalized;
    }
}
