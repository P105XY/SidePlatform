using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{
    private Vector3 mDestination;
    private float mMovementSpeed;
    private float mDistance = 1.0f;

    private GameObject mPlayerObject;
    private GameObject mLastNode;

    private bool mIsDone = false;

    [field: SerializeField]
    private GameObject mNodePrefab;


    // Start is called before the first frame update
    void Start()
    {
        mMovementSpeed = 0.25f;
        mPlayerObject = PlayerManager.GetInstance.PlayerObject;
        mLastNode = transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
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

    private void FixedUpdate()
    {

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
