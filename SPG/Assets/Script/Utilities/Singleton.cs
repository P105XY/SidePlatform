using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T minstance;
    private static object mLock = new object();
    private static bool mIsLockObject = false;

    public static T GetInstance
    {
        get
        {
            if (mIsLockObject)
            {
                Debug.LogWarning($"[Singleton] is lock, {typeof(T)}  return null");
                return null;
            }

            //여기서 lock을 해서 다른 오브젝트에서 접근을 못하게 만듦
            lock (mLock)
            {
                if(minstance == null)
                {
                    minstance = (T)FindObjectOfType(typeof(T));

                    if(minstance == null)
                    {
                        var singletonObj = new GameObject();
                        minstance = singletonObj.AddComponent<T>();
                        singletonObj.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObj);
                    }
                }

                return minstance;
            }
        }
    }

    private void OnDestroy()
    {
        mIsLockObject = true;
    }
}
