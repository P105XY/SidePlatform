using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ObjectoPooling : MonoBehaviour
{
    public List<GameObject> PoolObjects;
    public List<GameObject> PoolActiveObjects;

    private Action<GameObject> ActiveFalse;

    // Start is called before the first frame update
    void Start()
    {
        PoolObjects = new List<GameObject>();
        ActiveFalse = delegate (GameObject gameobj)
        {
            gameobj.SetActive(false);
        };

        foreach (var v in PoolObjects)
        {
            GameObject newGameobj = Instantiate(v, transform, true);
            ActiveFalse(newGameobj);
            PoolActiveObjects.Add(Instantiate(newGameobj, transform, true));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }    private void SetActiveFalse(GameObject gameobject)
    {
        
    }
}
