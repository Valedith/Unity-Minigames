using Assets.Scripts.Constants;
using System.Collections;
using UnityEngine;

public class RexMovement : MonoBehaviour {
    float jumpPower;
    bool onGround;
    Animator rexAnimator;

    private static bool isStart;
    private static bool isDead;
    private static bool isWaiting;
    Rigidbody2D rexRigidBody2D;

    public static bool IsStart
    {
        get
        {
            return isStart;
        }

        set
        {
            isStart = value;
        }
    }

    public static bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    public static bool IsWaiting
    {
        get
        {
            return isWaiting;
        }

        set
        {
            isWaiting = value;
        }
    }

    // Use this for initialization
    void Start () {
        Init();
    }
	
	// Update is called once per frame
	void Update () {
        if(IsDead)
        {
            GameOver();
        }
        else
        {
            Jump();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LAND")
        {
            onGround = true;
            rexAnimator.SetBool("isJumping", false);
        }
    }
    void Init()
    {
        rexRigidBody2D = GetComponent<Rigidbody2D>();
        rexAnimator = GetComponent<Animator>();
        jumpPower = PlayerMovementConstants.PLAYER_JUMP_POWER;
        onGround = true;
        isDead = false;
        IsStart = false;
        isWaiting = false;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            //Add Jumping Sound
            rexAnimator.SetBool("isJumping", true);
            rexRigidBody2D.velocity = new Vector2(rexRigidBody2D.velocity.x, jumpPower);
            onGround = false;
            StartCoroutine(GameStart());
        }
    }
    void GameOver()
    {
        if(IsDead)
        {
            isStart = false;
            rexAnimator.SetBool("isJumping", false);
            rexAnimator.SetBool("isDead", true);
            rexRigidBody2D.freezeRotation = true;
            rexRigidBody2D.velocity = Vector2.zero;
            rexRigidBody2D.gravityScale = 0;
        }
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.0f);
        if (IsStart == false)
        {
            IsStart = true;
        }
    }
}
