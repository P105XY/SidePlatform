using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    Dictionary<KeyCode, Action> KeyInput;
    Dictionary<KeyCode, Action> KeyRelease;
    public bool mIsActiveInput { get; private set; }

    public void Initialize()
    {
        SetKeyInput();
        SetKeyRelease();
    }

    private void SetKeyInput()
    {
        KeyInput = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Space, Jump},
            { KeyCode.Mouse0, Shoot},
            { KeyCode.Mouse1,  Grapple}
        };
    }

    private void SetKeyRelease()
    {
        KeyRelease = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Space, StopJump},
            { KeyCode.Mouse0, StopShoot},
            { KeyCode.Mouse1,  StopGrapple}
        };
    }

    private void Update()
    {
        if (!mIsActiveInput) return;

        foreach (var key in KeyInput)
            if (Input.GetKeyDown(key.Key))
                key.Value();

        foreach (var key in KeyRelease)
            if (Input.GetKeyUp(key.Key))
                key.Value();


    }

    private void Jump()
    {

    }

    private void StopJump()
    {

    }

    private void Shoot()
    {

    }

    private void StopShoot()
    {

    }

    private void Grapple()
    {

    }

    private void StopGrapple()
    {

    }

    public void SetActive(bool isTrueFalse)
    {
        this.mIsActiveInput = isTrueFalse;
    }
}
