using System.Collections.Generic;
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

    private Vector2 mGunRotateCenter;
    private Vector2 mMousePosition;
    private Vector2 mShootingDirection;

    private void Start()
    {
        IsGrappling = false;
        PlayerObject = PlayerManager.GetInstance.PlayerObject;
        mGunlist = PlayerManager.GetInstance.GetGunList();
        mGunlist[0].SetActive(true);

        currentHook = Instantiate(Hook, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        SetGunOrbitData();
    }

    public void GrappleAction()
    {
        if (IsGrappling)
            StopGrappleAction();

        IsGrappling = true;

        mShootingDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        currentHook.SetActive(true);
        currentHook.GetComponent<GrappleAction>().SetDestination(mShootingDirection);
    }

    public void StopGrappleAction()
    {
        currentHook.GetComponent<DistanceJoint2D>().connectedBody = null;
        IsGrappling = false;
        currentHook.SetActive(false);
    }

    public void ShootingGun()
    {
        if (mGunlist[mCurrentChooseGun].TryGetComponent<GunBase>(out GunBase comp))
            comp.SettingShooting(true);
    }

    public void StopShootingGun()
    {
        if (mGunlist[mCurrentChooseGun].TryGetComponent<GunBase>(out GunBase comp))
            comp.SettingShooting(false);
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

    public void AfterBunner()
    {

    }

    public void StopAtferBunner()
    {

    }



    public Vector2 GetGrapplePosition()
    {
        return currentHook.transform.position;
    }
}
