using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerInput PlayerInput { get; private set; }
    public PlayerStatus PlayerStatus { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public GameObject PlayerObject { get; private set; }


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        PlayerObjectInit();

        PlayerInput = GetComponent<PlayerInput>();
        PlayerStatus = GetComponent<PlayerStatus>();

        PlayerInput.Initialize();
        PlayerStatus.Initialize();
    }

    public void PlayerObjectInit()
    {
        PlayerObject = GameObject.FindGameObjectWithTag(GlobalString.GlobalPlayer);
        PlayerAction = PlayerObject.GetComponent<PlayerAction>();
        PlayerMovement = PlayerObject.GetComponent<PlayerMovement>();
    }
}
