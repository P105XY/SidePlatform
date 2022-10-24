using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<TabbedView>
{
    private static Singleton<T> instance;

    public static Singleton<T> Instnacing
    {
        get
        {
            if (instance == null)
                instance = new Singleton<T>();
            return instance;
        }
    }
}
