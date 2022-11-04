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
    void IHit.HitEnemy()
    {
        throw new System.NotImplementedException();
    }

    void IHit.HitObject()
    {
        throw new System.NotImplementedException();
    }

}
