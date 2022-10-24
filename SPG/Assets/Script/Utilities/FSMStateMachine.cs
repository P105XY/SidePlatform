using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMStateMachine<T> where T : class
{
    private T mOwnerEntity;
    private FSMState<T> mCurrentState;

    public void ChangeState(FSMState<T> state)
    {
        if (mOwnerEntity == null) return;

        mCurrentState.Exit(mOwnerEntity);
        mCurrentState = state;
        mCurrentState.Enter(mOwnerEntity);

    }

    public void DelayChangeState(FSMState<T> state, float Delay)
    {

    }

    public void Initialize(T owner, FSMState<T> state)
    {
        mOwnerEntity = owner;
        mCurrentState = state;

        ChangeState(mCurrentState);
    }

    public void Excute()
    {
        if (mCurrentState != null)
            mCurrentState.Excute(mOwnerEntity);
    }

    public FSMState<T> GetCurrentState()
    {
        return mCurrentState;
    }

    public bool CompareCurrentState(FSMState<T> state)
    {
        return mCurrentState == state;
    }
}
