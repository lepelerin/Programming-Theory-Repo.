using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerControl : GeneralControl
{
    [SerializeField] GameObject targetCamera;
    [SerializeField] GameObject Followingcamera;
    [SerializeField] float zoomSpeed;
    [SerializeField] float Zoomposition;

    [SerializeField] float speed;
    private float initialSpeed;
    [SerializeField] float rotaionSpeed;

    [SerializeField] GameObject ghost;

    private Animator animatorGhost;
    private AudioSource audioSourceGhost;
    // Start is called before the first frame update

    public delegate void Notify(Vector3 position);
    public event Notify ScaringHumans;

    private bool IsScared = false;
    private List<GameObject> cats = new List<GameObject>();

    private Transform obstruction;
    private MenuPauseHandler menuPauseHandler;

    void Start()
    {
        targetCamera.transform.rotation = transform.rotation;
        obstruction = transform;
        animatorGhost = ghost.GetComponent<Animator>();
        audioSourceGhost = GetComponent<AudioSource>();
        initialSpeed = speed;
        menuPauseHandler = GameObject.Find("MenuPauseHandler").GetComponent<MenuPauseHandler>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!menuPauseHandler.IsPaused)
        {
            ScareHuman();
            RestartLevel();
        }
    }

    void FixedUpdate()
    {
        RotateCamera();
        MovePlayer();
        ScaredMove();
    }
    void LateUpdate()
    {
        PositionCamera();
        ViewObstructed();
    }
    void PositionCamera()
    {
        targetCamera.transform.position = transform.position;
    }
    void ViewObstructed()
    {
        if (Physics.Raycast(Followingcamera.transform.position, transform.position - Followingcamera.transform.position, out RaycastHit hit, 10f))
        {
            //Debug.Log(hit.collider.gameObject+" ////" + hit.collider.gameObject.layer.ToString());
            if (!hit.collider.gameObject.CompareTag("Player"))
            {
                if (obstruction != hit.transform)
                {
                    obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    obstruction = hit.transform;
                }
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                if (Vector3.Distance(obstruction.position, Followingcamera.transform.position) >= 3f && Vector3.Distance(Followingcamera.transform.position, transform.position) >= 1.5f)
                {
                    Followingcamera.transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                }

            }
            else
            {

                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(Followingcamera.transform.position, transform.position) < Zoomposition)
                {
                    Followingcamera.transform.Translate(Vector3.back * 2f * Time.deltaTime);
                }
            }
        }
    }

    void RotateCamera()
    {
        float inputRotational = Input.GetAxis("Rotational");
        targetCamera.transform.Rotate(Vector3.up * Time.deltaTime * inputRotational * rotaionSpeed);
    }



    //ABSTRACTION
    void MovePlayer()
    {
        if (!IsScared)
        {
            if (FaceWall())
                speed = 0;
            else speed = initialSpeed;

            float inputVertical = Input.GetAxis("Vertical");
            float inputHorizontal = Input.GetAxis("Horizontal");
            float angle = Mathf.Atan2(inputHorizontal, inputVertical) * Mathf.Rad2Deg;
            if (inputHorizontal != 0 || inputVertical != 0)
            {
                animatorGhost.SetBool("IsMoving", true);
                transform.rotation = targetCamera.transform.rotation;
            }
            else
            {
                animatorGhost.SetBool("IsMoving", false);
            }
            transform.Translate(Vector3.forward * Time.deltaTime * inputVertical * speed);
            transform.Translate(Vector3.right * Time.deltaTime * inputHorizontal * speed);
            transform.Rotate(Vector3.up * angle);

        }
    }

    bool FaceWall()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out RaycastHit hit, 0.5f))
        {
            return hit.collider.gameObject.CompareTag("Wall");
        }
        return false;
    }

    void ScareHuman()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            audioSourceGhost.Play();
            if (cats.Count > 0)
            {
                Vector3 CatsVector = Vector3.zero;
                for (int i = 0; i < cats.Count; i++)
                {
                    AudioSource audioSource = cats[i].GetComponent<AudioSource>();
                    audioSource.Play();
                    CatsVector += cats[i].transform.position;
                }
                Scared(CatsVector);
            }
            else
            {
                animatorGhost.Play("attack_shift");
                ScaringHumans?.Invoke(transform.position);
            }
        }
    }

    private void RestartLevel()
    {
        if (Input.GetButtonDown("Fire2"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            ScaringHumans += other.gameObject.GetComponentInParent<Humans>().Scared;
        }
        if (other.gameObject.CompareTag("Cat"))
        {
            cats.Add(other.gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Human"))
        {
            ScaringHumans -= other.gameObject.GetComponentInParent<Humans>().Scared;
        }
        if (other.gameObject.CompareTag("Cat"))
        {
            cats.Remove(other.gameObject.transform.parent.gameObject);
        }
    }

    public override void Scared(Vector3 position)
    {

        RotateToward(position, true);

        IsScared = true;
        animatorGhost.SetBool("IsMoving", true);
        StartCoroutine(ScaredNoMore());
    }
    IEnumerator ScaredNoMore()
    {
        yield return new WaitForSeconds(1);
        IsScared = false;
        animatorGhost.SetBool("IsMoving", false);
    }
    private void ScaredMove()
    {
        if (IsScared)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }



    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
