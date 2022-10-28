using System.Collections;
using System.Collections.Generic;
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
        Vector2 dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentHook = Instantiate(Hook, transform.position, Quaternion.identity);
        currentHook.GetComponent<GrappleAction>().SetDestination(dest);
        IsGrappling = true;
    }

    public void StopGrappleAction()
    {

    }

    public void ShootingGun()
    {

    }
    public void StopShootingGun()
    {

    }

}
