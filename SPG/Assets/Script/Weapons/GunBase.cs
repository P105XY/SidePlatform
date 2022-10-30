using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    protected string mWeaponName;
    protected float mWeaponRange;
    protected float mWeaponDamage;
    protected float mWeaponRate;


    public void InitializeGun(string name,float range, float damage, float rate)
    {
        mWeaponName = name;
        mWeaponRange = range;
        mWeaponDamage = damage;
        mWeaponRate = rate;
    }

    public void ShootWeapon()
    {

    }

}
