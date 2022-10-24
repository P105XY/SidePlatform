using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState<T> where T : class
{
    public abstract void Enter(T owner);
    public abstract void Excute(T owner);
    public abstract void Exit(T owner);

}
