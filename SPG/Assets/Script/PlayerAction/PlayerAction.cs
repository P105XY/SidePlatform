using System.Collections;
using System.Collections.Generic;
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

    private Vector2 mGunRotateCenter;
    private Vector2 mMousePosition;



    private void Start()
    {
        IsGrappling = false;
        PlayerObject = PlayerManager.GetInstance.PlayerObject;
        mGunlist = PlayerManager.GetInstance.GetGunList();
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
        Vector2 dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentHook = Instantiate(Hook, transform.position, Quaternion.identity);
        currentHook.GetComponent<GrappleAction>().SetDestination(dest);
    }

    public void StopGrappleAction()
    {
        currentHook.GetComponent<DistanceJoint2D>().connectedBody = null;
        Destroy(currentHook);
        IsGrappling = false;
    }

    public void ShootingGun()
    {
    }

    public void StopShootingGun()
    {

    }

    public void SwapPrevWeapon()
    {

    }

    public void SwapNextWeapon()
    {

    }

    private void SetGunOrbitData()
    {
        mGunRotateCenter = PlayerObject.transform.position;
        mMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 p = (mMousePosition - mGunRotateCenter).normalized;
        float angle = Mathf.Atan2(p.y, p.x) * Mathf.Rad2Deg;


        foreach (var v in mGunlist)
        {
            v.transform.localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            v.transform.localPosition = Vector3.zero + (Vector3)p;
        }


    }

    public Vector2 GetGrapplePosition()
    {
        return currentHook.transform.position;
    }
}
