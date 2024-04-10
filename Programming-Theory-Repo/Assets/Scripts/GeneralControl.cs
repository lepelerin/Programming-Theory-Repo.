using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class GeneralControl : MonoBehaviour
{
    [SerializeField] protected float forceMultiplicator;
    protected List<GameObject> gameObjectsInRage = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
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
    public abstract void Scared(Vector3 position);
    
        
    
    protected Vector3 GetScaredDirection(Vector3 position)
    {
        return (transform.position - position).normalized;
    }
    protected void RotateToward(Vector3 position)
    {
        RotateToward(position,true);
    }
    protected void RotateToward(Vector3 position,bool opposite)
    {
        position.y = 0;
        transform.LookAt(position);
        transform.SetPositionAndRotation(transform.position, new Quaternion(0, transform.rotation.y, 0, transform.rotation.w));
        if (opposite)
        {
            transform.Rotate(Vector3.up * 180);
        }
    }
}
