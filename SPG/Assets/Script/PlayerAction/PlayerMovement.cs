using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float mLastLand;
    private float mLastJump;
    public bool isJumping;
    private Rigidbody2D mCurrentRigid;



    private void FixedUpdate()
    {
        mLastLand -= Time.deltaTime;
        mLastJump -= Time.deltaTime;
    }

    private void Update()
    {
        if (mLastLand > 0.0f && mLastJump > 0.0f && !isJumping)
        {

        }
    }


    public void Movement()
    {

    }

    public void JumpAction()
    {
        mLastJump = 0.1f;
    }

    public void StopJumpAction()
    {
        mLastJump = 0.0f;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalString.GlobalPlatform) ||
            collision.gameObject.CompareTag(GlobalString.GlobalGround) && mCurrentRigid.velocity.y <= 0.0f)
        {
            isJumping = false;
        }
        mLastLand = 0.1f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

}
