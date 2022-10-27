using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{
    private Vector2 mDestination;
    private float mMovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mMovementSpeed = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, mDestination, mMovementSpeed);
    }

    public void SetDestination(Vector2 dest) => mDestination = dest;
}
