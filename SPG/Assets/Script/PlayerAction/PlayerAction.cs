using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject Hook;
    private GameObject currentHook;

    private void Start()
    {

    }

    public void GrappleAction()
    {
        Vector2 dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(dest);
        currentHook = Instantiate(Hook, transform.position, Quaternion.identity);
        currentHook.GetComponent<GrappleAction>().SetDestination(dest);
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
