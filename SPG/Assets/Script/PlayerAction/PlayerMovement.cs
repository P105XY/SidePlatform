using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerMovement : MonoBehaviour
{
    private bool mIsActive;
    private bool mIsJumpMaxHeight;
    private float mLastLand;
    private float mLastJump;
    private Rigidbody2D mCurrentRigid;

    [field: HideInInspector]
    public bool isJumping;
    [field: HideInInspector]
    public float MoveDirection;

    [field: SerializeField]
    private float mMovementSpeepd;
    [field: SerializeField]
    private float mJumpPower;
    [field: SerializeField]
    private float mStopJumpDownForce;
    [field: SerializeField]
    private float mMaxJumpDownForce;
    [field: SerializeField]
    private float mAccelerationRate;
    [field: SerializeField]
    private float mDeccelerationRate;
    [field: SerializeField]
    private float mVelocityPower;
    [field: SerializeField]
    private float mSwingPower;
    [field: SerializeField]
    private float mRopeJumpPower;

    [field: SerializeField]
    private float mDownRay;

    private PlayerAction mPlayerAction;
    private RaycastHit2D mGroundHit;
    private Vector2 mGroundCheckBoxSize;

    private void Start()
    {
        mCurrentRigid = GetComponent<Rigidbody2D>();
        mPlayerAction = GetComponent<PlayerAction>();
        mGroundCheckBoxSize = new Vector2(5.0f, 0.1f);
    }

    private void FixedUpdate()
    {
        mIsActive = PlayerManager.GetInstance.PlayerInput.mIsActiveInput;

        if (!mIsActive) return;

        mLastLand -= Time.deltaTime;
        mLastJump -= Time.deltaTime;
    }

    private void Update()
    {
        if (!mIsActive) return;

        mGroundHit = Physics2D.BoxCast(transform.position, mGroundCheckBoxSize, 0.0f, Vector2.down, mDownRay, LayerMask.GetMask(GlobalString.GlobalGround));

        if (mGroundHit.collider != null)
        {
            if (mCurrentRigid.velocity.y <= 0.0f)
            {
                isJumping = false;
                mIsJumpMaxHeight = true;
            }
            mLastLand = 0.1f;
        }


        if (mLastLand > 0.0f && mLastJump > 0.0f && !isJumping && mGroundHit.collider)
        {
            mCurrentRigid.AddForce(Vector2.up * mJumpPower, ForceMode2D.Impulse);

            mLastLand = 0.0f;
            mLastJump = 0.0f;
            isJumping = true;
        }

        if (mCurrentRigid.velocity.y <= Mathf.Epsilon && mIsJumpMaxHeight && isJumping)
        {
            MaxJumpHeightAction();
            mIsJumpMaxHeight = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down*mDownRay, mGroundCheckBoxSize);
        Gizmos.DrawRay(transform.position, Vector2.down * mDownRay);
    }


    public void Movement(float direction)
    {
        if (direction != 0.0f) MoveDirection = direction;

        if (!mPlayerAction.IsGrappling)
        {
            float targetSpeed = mMovementSpeepd * direction;
            float speedDiffrence = targetSpeed - mCurrentRigid.velocity.x;
            float acceleration = (Mathf.Abs(targetSpeed) > 0.01f) ? mAccelerationRate : mDeccelerationRate;
            float movement = Mathf.Pow(Mathf.Abs(speedDiffrence) * acceleration, mVelocityPower) * Mathf.Sign(speedDiffrence);

            mCurrentRigid.AddForce(movement * Vector2.right);
        }
        else
        {
            mCurrentRigid.AddForce(direction * mSwingPower * Time.deltaTime * Vector2.right, 0);
        }

    }

    private float GetBothVectorTheta(Vector2 standard, Vector2 target)
    {
        return Quaternion.FromToRotation(Vector2.right, target - standard).eulerAngles.z;

        //float standardAbs = Mathf.Sqrt(Mathf.Pow(standard.x, 2) + Mathf.Pow(standard.y, 2));
        //float targetAbs = Mathf.Sqrt(Mathf.Pow(target.x, 2) + Mathf.Pow(target.y, 2));

        //return Mathf.Acos(Vector2.Dot(standard, target) / Mathf.Abs(standardAbs * targetAbs)) * Mathf.Rad2Deg;
    }

    public void StopMovement()
    {
        if (mLastLand > 0.0f)
        {
            float amount = Mathf.Min(Mathf.Abs(mCurrentRigid.velocity.x), Mathf.Abs(mCurrentRigid.sharedMaterial.friction));
            amount *= Mathf.Sign(mCurrentRigid.velocity.x);
            mCurrentRigid.AddForce(amount * Vector2.right, ForceMode2D.Impulse);
        }
    }

    public void JumpAction()
    {
        if (mPlayerAction.IsGrappling)
        {
            mPlayerAction.StopGrappleAction();
            mCurrentRigid.AddForce(new Vector2(mCurrentRigid.velocity.x, Mathf.Abs(mCurrentRigid.velocity.y)) * mRopeJumpPower, ForceMode2D.Impulse);
        }

        mLastJump = 0.1f;
    }

    public void StopJumpAction()
    {
        mLastJump = 0.0f;

        if (mCurrentRigid.velocity.y >= 0.0f && isJumping)
        {
            mCurrentRigid.AddForce(Vector2.down * mStopJumpDownForce * mCurrentRigid.velocity.y, ForceMode2D.Impulse);
        }
    }

    private void MaxJumpHeightAction()
    {
        mLastJump = 0.0f;
        mCurrentRigid.AddForce(Vector2.down * mMaxJumpDownForce, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

}
