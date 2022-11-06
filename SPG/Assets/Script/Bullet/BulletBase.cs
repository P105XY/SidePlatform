using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit
{
    void HitEnemy();
    void HitObject();
}

public class BulletBase : MonoBehaviour, IHit
{
    [field: SerializeField]
    protected float mMoveSpeed;
    protected Vector2 mMovementDirection = new();
    protected Rigidbody2D mCurRigid = new();

    public void SetDirection(Vector2 d)
    {
        mMovementDirection = d;
    }
    private void Start()
    {
        mCurRigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mCurRigid.MovePosition((Vector2)transform.position + mMovementDirection * mMoveSpeed * Time.deltaTime);
    }

    void IHit.HitEnemy()
    {
        throw new System.NotImplementedException();
    }

    void IHit.HitObject()
    {
        throw new System.NotImplementedException();
    }

}
