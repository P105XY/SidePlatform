using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject Hook;
    public bool IsGrappling;

    [HideInInspector]
    public GameObject PlayerObject;
    private GameObject currentHook;
    private int mCurrentChooseGun;
    private List<GameObject> mGunlist;
    private bool mIsShooting;
    private float mFireDelay;
    private IShootGun ig;

    private Vector2 mGunRotateCenter;
    private Vector2 mMousePosition;
    private Vector2 mShootingDirection;


    private void Start()
    {
        IsGrappling = false;
        PlayerObject = PlayerManager.GetInstance.PlayerObject;
        mGunlist = PlayerManager.GetInstance.GetGunList();
        mFireDelay = Time.time;
        mGunlist[0].SetActive(true);
    }

    private void Update()
    {
        SetGunOrbitData();

        if (mIsShooting && mGunlist[mCurrentChooseGun].TryGetComponent<GunBase>(out GunBase guns))
        {
            if (Time.time - mFireDelay > guns.GetRate())
            {
                Debug.Log(guns.GetRate());
                ig = guns;
                ig.Shooting(mShootingDirection);
                mFireDelay = Time.time;
            }
        }

    }

    public void GrappleAction()
    {
        if (IsGrappling)
            StopGrappleAction();

        IsGrappling = true;
        mShootingDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentHook = Instantiate(Hook, transform.position, Quaternion.identity);
        currentHook.GetComponent<GrappleAction>().SetDestination(mShootingDirection);
    }

    public void StopGrappleAction()
    {
        currentHook.GetComponent<DistanceJoint2D>().connectedBody = null;
        Destroy(currentHook);
        IsGrappling = false;
    }

    public void ShootingGun() => mIsShooting = true;

    public void StopShootingGun()
    {
        mIsShooting = false;
        mFireDelay = Time.time;
    }
    public void SwapPrevWeapon()
    {
        if (mCurrentChooseGun > 0)
        {
            mGunlist[mCurrentChooseGun].SetActive(false);
            mGunlist[--mCurrentChooseGun].SetActive(true);
        }
    }

    public void SwapNextWeapon()
    {
        if (mCurrentChooseGun < mGunlist.Count - 1)
        {
            mGunlist[mCurrentChooseGun].SetActive(false);
            mGunlist[++mCurrentChooseGun].SetActive(true);
        }
    }

    private void SetGunOrbitData()
    {
        mGunRotateCenter = PlayerObject.transform.position;
        mMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 p = (mMousePosition - mGunRotateCenter).normalized;
        float angle = Mathf.Atan2(p.y, p.x) * Mathf.Rad2Deg;

        mGunlist[mCurrentChooseGun].transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        mGunlist[mCurrentChooseGun].transform.localPosition = Vector3.zero + ((Vector3)p * 5.0f);
    }

    public Vector2 GetGrapplePosition()
    {
        return currentHook.transform.position;
    }
}
