using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : GunBase
{
    public override void Shooting(Vector2 dir)
    {
        base.Shooting(dir);
        Debug.Log("SHOOTING" + gameObject.name);
    }
}
