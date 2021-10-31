using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float MoveSpeed = 3f;

    public Animator animator;

    Vector3 Movement;

    int MovementFlag = 0;

    bool isTracing = false;

	void Start ()
    {
        StartCoroutine("ChangetoMovement");
        animator = gameObject.GetComponentInChildren<Animator>();
    }

     void FixedUpdate()
    {
        if (BtnManager.PauseCheck == true)
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

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (MovementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1f, 1f, 1f);

            transform.position += moveVelocity * MoveSpeed * Time.deltaTime;

            animator.SetBool("MoveAnimation", true);
        }
        else if (MovementFlag == 2)
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
