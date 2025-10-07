using System.Collections.Generic;
using System.Linq;

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
}

