using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootGun
{
    void Shooting(Vector2 dir);
}
public abstract class GunBase : MonoBehaviour, IShootGun
{
    public string mWeaponName;
    public float mWeaponRange;
    public float mWeaponDamage;
    public float mWeaponRate;


    public void InitializeGun(string name, float range, float damage, float rate)
    {
        Debug.Log("Init GUN");
        mWeaponName = name;
        mWeaponRange = range;
        mWeaponDamage = damage;
        mWeaponRate = rate;
    }

    public string GetName() { return mWeaponName; }
    public float GetDamage() { return mWeaponDamage; }
    public float GetRange() { return mWeaponRange; }
    public float GetRate() { return mWeaponRate; }

    public abstract void Shooting(Vector2 dir);

}
