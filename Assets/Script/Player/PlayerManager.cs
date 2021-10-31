using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float WalkSpeed;
    public float JumpPower;

    public static int health = 3;
    public int JumpCount = 0;

    public Animator animator;

    bool isDie = false;
    bool doubleJump = false;
    bool isJumping = false;

    public static bool isUnBeatTime = false;
    public static bool TimeSpanBool = false;

    SpriteRenderer spriterenderer;

    public static Rigidbody2D rigid;

    //float distance = 10; //DragMove
    //public Vector3 Target; //ClickMove

	void Start ()
    {
        spriterenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        //Target = transform.position;

    }

    void Update()
    {
        if (BtnManager.PauseCheck == true)
        {
            return;
        }
        else
        {

            if (WeaponManager.LeftMouseOn == false)
            {
                animator.SetBool("MouseLeft", true);
                animator.SetBool("MouseRight", false);
            }
            else
            {
                animator.SetBool("MouseLeft", false);
                animator.SetBool("MouseRight", true);
            }

            if ((Input.GetButtonDown("Jump") && animator.GetBool("IsJumping") == false) || (animator.GetBool("IsJumping") == true && doubleJump == true))
            {
                JumpCount++;
                isJumping = true;
                animator.SetTrigger("IsJumping");
                animator.SetBool("IsJumping", true);

                if (JumpCount == 2) // 더블점프 (수정 해야함)
                {
                    doubleJump = false;
                    JumpCount = 0;
                }
            }
        }
    }

    void FixedUpdate ()
    {
        if (BtnManager.PauseCheck == true)
        {
            return;
        }
        else
        {
            move_3();
            Jump();
        }
    }

    void move_3() //AD Grav = 1
    {
        if (Input.GetKey(KeyCode.A)) //왼쪽으로 갈때
        {
            transform.Translate(Vector3.left * WalkSpeed * Time.deltaTime);
            animator.SetInteger("Duration", -1);
            animator.SetBool("IsMoving", true);           
        }
        else

        if (Input.GetKey(KeyCode.D)) //오른쪽으로 갈떄
        {
            transform.Translate(Vector3.right * WalkSpeed * Time.deltaTime);
            animator.SetInteger("Duration", 1);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void Jump()
    {  
        if (!isJumping)
            return;

        rigid.velocity = Vector2.zero;

        Vector2 JumpVelocity = new Vector2(0, JumpPower*4.75f);
        rigid.AddForce(JumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Creature" && !other.isTrigger && rigid.velocity.y < 0f)//크리쳐 점프 충돌
        {          
            DieCont creature = other.gameObject.GetComponent<DieCont>();
            creature.Die();

            Vector2 KillVelocity = new Vector2(0, 12f);
            rigid.AddForce(KillVelocity, ForceMode2D.Impulse);

            ScoreManager.SetScore(creature.score);            
        }
        else if(other.gameObject.tag == "Creature" && !other.isTrigger && rigid.velocity.y <= 0f && isUnBeatTime == false) //크리처 충돌
        {
            Vector2 attackedVelocity = Vector2.zero;
            if (other.gameObject.transform.position.x > transform.position.x) //충돌 방향 찾기
                attackedVelocity = new Vector2(-2f, 5f);
            else
            {
                attackedVelocity = new Vector2(2f, 5f);
            }
            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);

            health--;

            if (health >= 1)
            {
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime");
            }
        }

        if (other.gameObject.tag == "Coin") //코인 충돌
        {
            DieCont Coin = other.gameObject.GetComponent<DieCont>();
            Coin.Die();
            ScoreManager.SetScore(Coin.score);
        }

        if((other.gameObject.layer == 8 || other.gameObject.layer ==9) && rigid.velocity.y <= 0)
        {
            animator.SetBool("IsJumping", false);

            if(other.gameObject.layer == 9 && rigid.velocity.y <= 0)
            {
                BlockStatus block = other.gameObject.GetComponent<BlockStatus>();

                switch (block.Type)
                {
                    case "Up":
                        Vector2 JumpVelocity = new Vector2(0, block.Value);
                        rigid.AddForce(JumpVelocity, ForceMode2D.Impulse);
                        break;

                    case "DoubleJump":
                        doubleJump = true;
                        break;
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Creature" && !other.isTrigger && rigid.velocity.y <= 0f && isUnBeatTime == false) //크리처 충돌
        {
            Vector2 attackedVelocity = Vector2.zero;
            if (other.gameObject.transform.position.x > transform.position.x) //충돌 방향 찾기
                attackedVelocity = new Vector2(-2f, 5f);
            else
            {
                attackedVelocity = new Vector2(2f, 5f);
            }
            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);

            health--;

            if (health >= 1)
            {
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime");
            }
        }
    }

    IEnumerator UnBeatTime()//맞을시 잠시동안 무적시간
    {
        int countTime = 0;

        while (countTime < 5)
        {
            if(countTime%2 == 0)
            {
                spriterenderer.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                spriterenderer.color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.15f);

            countTime++;
        }
        spriterenderer.color = new Color32(255, 255, 255, 255);

        isUnBeatTime = false;

        yield return null;
    }


    /* 
     void OnMouseDrag() //DragMove
     {
         print("Drag!!");
         Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
         Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
         transform.position = objPosition;
     }


     void move_3()
     {
         if (Input.GetMouseButtonDown(0))
         {
             Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             Target.z = transform.position.z;
         }
         transform.position = Vector3.MoveTowards(transform.position, Target, WalkSpeed * Time.deltaTime);
     }
     */
}
