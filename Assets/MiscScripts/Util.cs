using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Mathematics;

public static class Util
{
    public static bool In<T>(this T o, params T[] values)
    {
        if (values == null)
        {
            return false;
        }
        return values.Contains(o);
    }

    public static bool In<T>(this T o, IEnumerable<T> values)
    {
        if (values == null)
        {
            return false;
        }

        return values.Contains(o);
    }

    public static void GetChildrenWithTag(Transform parent, string tag, List<GameObject> list)
    {
        if (parent == null || string.IsNullOrEmpty(tag) == true)
        {
            throw new System.ArgumentNullException();
        }

        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == tag)
            {
                list.Add(child.gameObject);
            }
            GetChildrenWithTag(child, tag, list);
        }
    }

    public static int BoolToNegativePositiveOne(bool b)
    {
        if (b)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public static Vector2 Vec3toAbsVec2(Vector3 v)
    {
        Vector3 absScale3 = math.abs(v);
        Vector2 absScale2 = new Vector2(absScale3.x, absScale3.y);
        return absScale2;
    }
}
