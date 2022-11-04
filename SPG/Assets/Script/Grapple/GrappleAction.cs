using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{
    private Vector3 mDestination;
    private float mMovementSpeed;
    private float mDistance = 1.0f;
    private float mGrappleToPlayerDist;

    private GameObject mPlayerObject;
    private GameObject mLastNode;

    private bool mIsDone = false;

    [field: SerializeField]
    private GameObject mNodePrefab;
    [field: SerializeField]
    private AnimationCurve mLineAnimCurve;

    private LineRenderer mLineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        mMovementSpeed = 2.0f;
        mPlayerObject = PlayerManager.GetInstance.PlayerObject;
        mLastNode = transform.gameObject;
        mLineAnimCurve = new AnimationCurve();
        mLineRenderer = GetComponent<LineRenderer>();

        mLineRenderer.positionCount = 2;
        mLineRenderer.startWidth = mLineRenderer.endWidth = 0.08f;
        mLineRenderer.SetPosition(0, transform.position);
        mLineRenderer.SetPosition(1, PlayerManager.GetInstance.PlayerObject.transform.position);
        mLineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        DrawGrappleRope();
        mGrappleToPlayerDist = Vector2.Distance(mPlayerObject.transform.position, transform.position);

        if (!mIsDone)
        {
            if (mGrappleToPlayerDist >= 50.0f)
            {
                mIsDone = true;
            }

            transform.position = Vector2.MoveTowards(mPlayerObject.transform.position, mDestination.normalized * 50.0f, mMovementSpeed);
        }
        else
        {
            mLastNode.GetComponent<DistanceJoint2D>().connectedBody = mPlayerObject.GetComponent<Rigidbody2D>();
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

    public void SetDestination(Vector2 dest) => mDestination = dest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalString.GlobalGround) ||
            collision.gameObject.CompareTag(GlobalString.GlobalPlatform) ||
            collision.gameObject.CompareTag(GlobalString.GlobalWall) ||
            collision.gameObject.CompareTag(GlobalString.GlobalCeling))
        {
            mIsDone = true;
        }
    }
}
