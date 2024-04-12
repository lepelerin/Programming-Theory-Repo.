using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Humans : GeneralControl
{
    [SerializeField] float speed;
    private List<GameObject> dogs = new List<GameObject>();
    private AudioSource audioSourceHuman;

    [SerializeField] GameObject HumanMesh;
    private Animator animatorHuman;
    private bool isScared = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceHuman = GetComponent<AudioSource>();
        animatorHuman = HumanMesh.GetComponent<Animator>();
        Debug.Log(SaveManager.Instance.GetPlayer().scene +"Start");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isScared)
        {
            animatorHuman.SetFloat("speed_f", speed);
            animatorHuman.Play("m_run");
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else
        {
            animatorHuman.SetFloat("speed_f", 0);
        }
    }
    void RotateWhenFaceWall(Collision collision)
    {
/*

        float angle = Vector3.Angle(collision.GetContact(0).normal, -transform.forward);
        Debug.Log(angle);
        transform.Rotate(Vector3.up * (angle + 90));
*/
        transform.rotation = Quaternion.LookRotation(collision.GetContact(0).normal, transform.up);
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
        if (dogs.Count > 0)
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
        if (collision.gameObject.CompareTag("Door"))
        {
            //Destroy(gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            RotateWhenFaceWall(collision);
        }
    }

    private void OnDestroy()
    {
        SaveManager.Instance.LoadLevel(SaveManager.Instance.GetPlayer().scene + 1);
    }
}
