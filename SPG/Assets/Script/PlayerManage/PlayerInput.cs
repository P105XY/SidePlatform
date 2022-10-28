using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Dictionary<KeyCode, Action> mKeyInput;
    private Dictionary<KeyCode, Action> mKeyRelease;

    private Action mPlayerJump;
    private Action mPlayerStopJump;
    private Action mPlayerShooting;
    private Action mPlayerStopShooting;
    private Action mPlayerGrapple;
    private Action mPlayerStopGrapple;
    private Action<float> mPlayerMovement;
    private Action mPlayerStopMovement;

    public bool mIsActiveInput { get; private set; }

    public void Initialize()
    {
        SetKeyInput();
        SetKeyRelease();

        mPlayerJump += PlayerManager.GetInstance.PlayerMovement.JumpAction;
        mPlayerStopJump += PlayerManager.GetInstance.PlayerMovement.StopJumpAction;
        mPlayerMovement += PlayerManager.GetInstance.PlayerMovement.Movement;
        mPlayerStopMovement += PlayerManager.GetInstance.PlayerMovement.StopMovement;
        mPlayerShooting += PlayerManager.GetInstance.PlayerAction.ShootingGun;
        mPlayerStopShooting += PlayerManager.GetInstance.PlayerAction.StopShootingGun;
        mPlayerGrapple += PlayerManager.GetInstance.PlayerAction.GrappleAction;
        mPlayerStopGrapple += PlayerManager.GetInstance.PlayerAction.StopGrappleAction;
        mIsActiveInput = true;
    }

    private void SetKeyInput()
    {
        mKeyInput = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Space, Jump},
            { KeyCode.Mouse0, Shoot},
            { KeyCode.Mouse1,  Grapple},
        };
    }

    private void SetKeyRelease()
    {
        mKeyRelease = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Space, StopJump},
            { KeyCode.Mouse0, StopShoot},
            { KeyCode.A, PlayerStopMove },
            { KeyCode.D, PlayerStopMove }
        };
    }

    private void Update()
    {
        if (!mIsActiveInput) return;

        foreach (var key in mKeyInput)
            if (Input.GetKeyDown(key.Key))
                key.Value();

        foreach (var key in mKeyRelease)
            if (Input.GetKeyUp(key.Key))
                key.Value();
    }

    private void FixedUpdate()
    {
        mPlayerMovement(Input.GetAxisRaw("Horizontal"));
    }

    //one line funcs
    private void Jump() => mPlayerJump();
    private void StopJump() => mPlayerStopJump();
    private void Shoot() => mPlayerShooting();
    private void StopShoot() => mPlayerStopShooting();
    private void Grapple() => mPlayerGrapple();
    private void StopGrapple() => mPlayerStopGrapple();
    private void PlayerStopMove() => mPlayerStopMovement();
    public void SetActive(bool isTrueFalse) => this.mIsActiveInput = isTrueFalse;
}
