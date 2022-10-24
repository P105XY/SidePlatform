using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerState
{
    Idle, Move,Jump,Shoot,Grapple, Dead
}

public class PlayerFSMManager : MonoBehaviour
{
    private FSMState<PlayerFSMManager> mFSMState;
    private FSMStateMachine<PlayerFSMManager> mFSMMachine;
}


public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerInput PlayerInput { get; private set; }
    public PlayerStatus PlayerStatus { get; private set; }
    public GameObject PlayerObject { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        PlayerInput = new PlayerInput();
        PlayerStatus = new PlayerStatus();

        PlayerInput.Initialize();
        PlayerStatus.Initialize();
        PlayerAction = PlayerObject.GetComponent<PlayerAction>();
        PlayerMovement = PlayerObject.GetComponent<PlayerMovement>();
    }
}
