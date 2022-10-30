using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnSceneLoaded(Scene currentScene, LoadSceneMode loadedScene)
    {
        //PlayerManager.GetInstance.PlayerObjectInit();
    }
}
