using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public enum ePlayerState
{
    Idle,
    Move,
    Jump,
    Shoot,
    Grapple,
    Dead
}

public class PlayerFSMManager : MonoBehaviour
{
    private FSMState<PlayerFSMManager>[]        mFSMState;
    private FSMStateMachine<PlayerFSMManager>   mFSMMachine;
    private bool                                misAction;

    public void Initialize()
    {
        mFSMState = new FSMState<PlayerFSMManager>[6];
        mFSMState[(int)ePlayerState.Idle] = new playerFSM.Idle();
        mFSMState[(int)ePlayerState.Move] = new playerFSM.Idle();
        mFSMState[(int)ePlayerState.Jump] = new playerFSM.Idle();
        mFSMState[(int)ePlayerState.Shoot] = new playerFSM.Idle();
        mFSMState[(int)ePlayerState.Grapple] = new playerFSM.Idle();
        mFSMState[(int)ePlayerState.Dead] = new playerFSM.Idle();

        mFSMMachine.Initialize(this, mFSMState[(int)ePlayerState.Idle]);
        misAction = true;
    }

    private void FixedUpdate()
    {
        if (misAction) mFSMMachine.Excute();
    }
}