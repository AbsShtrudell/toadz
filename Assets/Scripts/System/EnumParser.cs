using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class EnumParser<T> where T : struct
{
    static public T Parse(string value)
    {
        T result;
        System.Enum.TryParse<T>(value, out result);

        return result;
    }
}
