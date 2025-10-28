using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
}
