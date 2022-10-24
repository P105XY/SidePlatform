using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YieldInstructionCache
{
    /// <summary>
    /// IEqualityComparer를 사용해서 두 값을 비교한다.
    /// </summary>
    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame CustoWaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate CustomWaitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());
    public static WaitForSeconds CustomWaitForSeconds(float sec)
    {
        WaitForSeconds wfs;
        if (!timeInterval.TryGetValue(sec, out wfs))
        {
            timeInterval.Add(sec, wfs = new WaitForSeconds(sec));
        }
        return wfs;
    }
}