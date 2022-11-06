using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{
    private Vector3 mDestination;
    private float mDistance = 1.0f;
    private float mGrappleToPlayerDist;
    private float mReturnElapse;
    [field: SerializeField]
    private float mGrappleReturnTime;
    [field: SerializeField]
    private float mMovementSpeed;
    [field: SerializeField]
    private float mGrappleMaxLengh;

    private GameObject mPlayerObject;
    private GameObject mLastNode;

    private bool mIsActiveGrapple;
    private bool mIsHitted;

    [field: SerializeField]
    private GameObject mNodePrefab;
    [field: SerializeField]
    private AnimationCurve mLineAnimCurve;
    private Rigidbody2D mGrappleRigid;

    private LineRenderer mLineRenderer;

    public void SetDestination(Vector2 dest)
    {
        transform.position = mPlayerObject.transform.position;
        mDestination = dest;
        mIsHitted = false;
        mIsActiveGrapple = true;
        mReturnElapse = 0.0f;
    }
    void Start()
    {
        mPlayerObject = PlayerManager.GetInstance.PlayerObject;
        mLastNode = transform.gameObject;
        mLineAnimCurve = new AnimationCurve();
        mLineRenderer = GetComponent<LineRenderer>();
        mGrappleRigid = GetComponent<Rigidbody2D>();

        mLineRenderer.positionCount = 2;
        mLineRenderer.startWidth = mLineRenderer.endWidth = 0.08f;
        mLineRenderer.SetPosition(0, transform.position);
        mLineRenderer.SetPosition(1, PlayerManager.GetInstance.PlayerObject.transform.position);
        mLineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        DrawGrappleRope();
        mGrappleToPlayerDist = Vector2.Distance(mPlayerObject.transform.position, transform.position);

        if (mIsActiveGrapple && !mIsHitted)
        {
            mGrappleRigid.MovePosition(transform.position + mDestination * mMovementSpeed * Time.deltaTime);

            if (mGrappleToPlayerDist >= mGrappleMaxLengh && !mIsHitted)
                mIsActiveGrapple = false;
        }

        if (mIsActiveGrapple && mIsHitted)
        {
            GetComponent<DistanceJoint2D>().connectedBody = mPlayerObject.GetComponent<Rigidbody2D>();
        }


        if (!mIsActiveGrapple && !mIsHitted && mReturnElapse < mGrappleReturnTime)
        {
            mReturnElapse += Time.deltaTime;
            mGrappleRigid.transform.position = Vector2.Lerp(transform.position, mPlayerObject.transform.position, mReturnElapse / mGrappleReturnTime);

            if (mGrappleToPlayerDist <= Mathf.Epsilon) PlayerManager.GetInstance.PlayerAction.StopGrappleAction();
        }
    }

    public void DrawGrappleRope()
    {
        mLineRenderer.SetPosition(0, transform.position);
        mLineRenderer.SetPosition(1, PlayerManager.GetInstance.PlayerObject.transform.position);
    }

    public void CreateNode()
    {
        Vector2 CreateNodePos = mPlayerObject.transform.position - mLastNode.transform.position;
        CreateNodePos.Normalize();
        CreateNodePos *= mDistance;
        CreateNodePos += (Vector2)mLastNode.transform.position;

        GameObject go = Instantiate(mNodePrefab, CreateNodePos, Quaternion.identity);
        go.transform.SetParent(transform);
        mLastNode.GetComponent<HingeJoint2D>().connectedBody = go.GetComponent<Rigidbody2D>();
        mLastNode = go;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag(GlobalString.GlobalGround) ||
            collision.gameObject.CompareTag(GlobalString.GlobalPlatform) ||
            collision.gameObject.CompareTag(GlobalString.GlobalWall) ||
            collision.gameObject.CompareTag(GlobalString.GlobalCeling) ||
            collision.gameObject.CompareTag(GlobalString.GlobalObstacle)) &&
            mIsActiveGrapple)
        {
            mIsHitted = true;
        }
    }
}
