using UnityEngine;
using System.Collections;

public static class BoundsEx
{
    /// <summary>
    /// 判断包围盒是否包含另一个包围盒
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="compareTo"></param>
    /// <returns></returns>
    public static bool ContainsEx(this Bounds bounds, Bounds compareTo)
    {
        float halfX = compareTo.size.x / 2;
        float halfY = compareTo.size.y / 2;
        float halfZ = compareTo.size.z / 2;

        if (!bounds.Contains(compareTo.center + new Vector3(-halfX, halfY, -halfZ)))
            return false;
        if (!bounds.Contains(compareTo.center + new Vector3(halfX, halfY, -halfZ)))
            return false;
        if (!bounds.Contains(compareTo.center + new Vector3(halfX, halfY, halfZ)))
            return false;
        if (!bounds.Contains(compareTo.center + new Vector3(-halfX, halfY, halfZ)))
            return false;
        if (!bounds.Contains(compareTo.center + new Vector3(-halfX, -halfY, -halfZ)))
            return false;
        if (!bounds.Contains(compareTo.center + new Vector3(halfX, -halfY, -halfZ)))
            return false;
        if (!bounds.Contains(compareTo.center + new Vector3(halfX, -halfY, halfZ)))
            return false;
        if (!bounds.Contains(compareTo.center + new Vector3(-halfX, -halfY, halfZ)))
            return false;
        return true;
    }
}
