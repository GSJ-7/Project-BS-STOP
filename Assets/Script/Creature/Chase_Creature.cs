using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Creature : MonoBehaviour
{

    public float MoveSpeed = 3f;
    
    Animator animator;

    Vector3 Movement;

    int MovementFlag = 0;

    bool isTracing = false;

    GameObject TraceTarget;

    void Start()
    {
        StartCoroutine("ChangetoMovement");
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if(BtnManager.PauseCheck == true)
        {
            return;
        }
        else
        {
            Move();
        }
    }

    IEnumerator ChangetoMovement()
    {
        MovementFlag = Random.Range(0, 3);

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangetoMovement");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            TraceTarget = other.gameObject;

            StopCoroutine("ChangetoMovement");
        }
    }

     void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isTracing = true;

        }
    }

     void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isTracing = false;

            StartCoroutine("ChangetoMovement");
        }
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if (isTracing)
        {
            Vector3 PlayerPos = TraceTarget.transform.position;

            if (PlayerPos.x < transform.position.x)
            {
                dist = "Left";
            }
            else if (PlayerPos.x > transform.position.x)
            {
                dist = "Right";
            }
        }

        if (MovementFlag == 1)
        {
            dist = "Left";
        }
        else if (MovementFlag == 2)
        {
            dist = "Right";
        }   

        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1f, 1f, 1f);

            transform.position += moveVelocity * MoveSpeed * Time.deltaTime;

            animator.SetBool("MoveAnimation", true);
        }
        else if (dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1f, 1f, 1f);

            transform.position += moveVelocity * MoveSpeed * Time.deltaTime;

            animator.SetBool("MoveAnimation", true);
        }
        else
        {
            animator.SetBool("MoveAnimation", false);

            transform.position += moveVelocity * 0 * Time.deltaTime;
        }
    }
}
