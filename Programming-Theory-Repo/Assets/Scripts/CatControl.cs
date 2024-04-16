using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatControl : GeneralControl
{
    public override void Scared(Vector3 position)
    {
        
    }


    [SerializeField] List<Vector3> positionNode;
    [SerializeField] GameObject catAnimator;
    private int currentNode;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = catAnimator.GetComponent<Animator>();
        currentNode = 0;
        if (positionNode.Count > 0)
        {
            RotateToward(positionNode[currentNode]);
            positionNode.Add(transform.position);
            animator.SetBool("IsWalking_B", true);
            animator.Play("Walk");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveCat();
    }

    void MoveCat()
    {
        if(positionNode.Count > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, positionNode[currentNode])<0.2)
            {
                currentNode++;
                if(currentNode==positionNode.Count) 
                    currentNode = 0;
                RotateToward(positionNode[currentNode]);

            }
        }
        
    }

}
