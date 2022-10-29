using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{
    private Vector3         mDestination;
    private float           mMovementSpeed;
    private float           mDistance = 1.0f;

    private GameObject      mPlayerObject;
    private GameObject      mLastNode;

    private bool            mIsDone = false;

    [field: SerializeField]
    private GameObject      mNodePrefab;
    [field: SerializeField]
    private AnimationCurve  mLineAnimCurve;

    private LineRenderer    mLineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        mMovementSpeed  = 2.0f;
        mPlayerObject   = PlayerManager.GetInstance.PlayerObject;
        mLastNode       = transform.gameObject;
        mLineAnimCurve  = new AnimationCurve();
        mLineRenderer   = GetComponent<LineRenderer>();

        mLineRenderer.positionCount = 2;
        mLineRenderer.startWidth = mLineRenderer.endWidth = 0.08f;
        mLineRenderer.SetPosition(0, transform.position);
        mLineRenderer.SetPosition(1, PlayerManager.GetInstance.PlayerObject.transform.position);
        mLineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        mLineRenderer.SetPosition(0, transform.position);
        mLineRenderer.SetPosition(1, PlayerManager.GetInstance.PlayerObject.transform.position);

        if (Vector2.Distance(transform.position, mDestination) <= Mathf.Epsilon && !mIsDone)
        {
            mIsDone = true;
            mLastNode.GetComponent<DistanceJoint2D>().connectedBody = mPlayerObject.GetComponent<Rigidbody2D>();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, mDestination, mMovementSpeed);
        }

        //transform.position = Vector2.MoveTowards(transform.position, mDestination, mMovementSpeed);
        //if (transform.position != mDestination)
        //{
        //    if (Vector2.Distance(mPlayerObject.transform.position, mLastNode.transform.position) > mDistance)
        //    {
        //        CreateNode();
        //    }
        //}
        //else if (mIsDone == false)
        //{
        //    mIsDone = true;
        //    mLastNode.GetComponent<HingeJoint2D>().connectedBody = mPlayerObject.GetComponent<Rigidbody2D>();
        //}

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

    public void SetDestination(Vector2 dest) => mDestination = dest;
}
