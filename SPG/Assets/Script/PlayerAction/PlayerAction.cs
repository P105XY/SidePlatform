using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject Hook;
    private GameObject currentHook;

    public bool IsGrappling;

    private void Start()
    {
        IsGrappling = false;
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
        Vector2 attackDir = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
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


    public Vector2 GetGrapplePosition()
    {
        return currentHook.transform.position;
    }
}
