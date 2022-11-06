using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootGun
{
    void Shooting(Vector2 dir);
}
public abstract class GunBase : MonoBehaviour, IShootGun
{
    protected string mWeaponName;
    protected float mWeaponRange;
    protected float mWeaponDamage;
    protected float mWeaponRate;
    protected Transform Muzzlepoint;

    public GameObject Bullet;

    private float mCheckFireRate;
    private GameObject mCurBullet;
    private bool mIsShooting;
    public void InitializeGun(string name, float range, float damage, float rate)
    {
        mWeaponName = name;
        mWeaponRange = range;
        mWeaponDamage = damage;
        mWeaponRate = rate;
        Muzzlepoint = transform.GetChild(0).transform;
    }

    private void Start()
    {
        mCheckFireRate = Time.time;

    }

    protected virtual void Update()
    {
        if (Time.time - mCheckFireRate > mWeaponRate && mIsShooting)
        {
            Shooting((Camera.main.ScreenToWorldPoint(Input.mousePosition) - Muzzlepoint.transform.position).normalized);
            mCheckFireRate = Time.time;
        }
    }

    public string GetName() { return mWeaponName; }
    public float GetDamage() { return mWeaponDamage; }
    public float GetRange() { return mWeaponRange; }
    public float GetRate() { return mWeaponRate; }

    public void SettingShooting(bool TF) => mIsShooting = TF;

    public virtual void Shooting(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        mCurBullet = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, angle));
        if (mCurBullet.TryGetComponent<BulletBase>(out BulletBase bullet)) bullet.SetDirection(dir);
    }

}
