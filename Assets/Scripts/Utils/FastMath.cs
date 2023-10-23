using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMath : MonoBehaviour
{
    public static float InvSqrt(float x)
    {
        // John Carmack's legendary algorithm
        float xhalf = 0.5f * x;
        int i = BitConverter.SingleToInt32Bits(x);
        i = 0x5f3759df - (i >> 1);
        x = BitConverter.Int32BitsToSingle(i);
        x *= 1.5f - xhalf * x * x;
        return x;
    }

    public static float Sqrt(float a)
    {
        return 1 / InvSqrt(a);
    }

    public static float Hypotenuse(float a, float b)
    {
        return Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
    }

    #region Distance
    public static float Distance(Vector2 a, Vector2 b)
    {
        return Magnitude(a - b);
    }

    public static float Distance(Vector3 a, Vector3 b)
    {
        return Magnitude(a - b);
    }
    #endregion

    #region Magnitude
    public static float Magnitude(Vector2 a)
    {
        return Hypotenuse(a.x, a.y);
    }

    public static float Magnitude(Vector3 a)
    {
        return Hypotenuse(a.x, a.y);
    }
    #endregion

    #region Normalize
    public static Vector2 Normalize(Vector2 a)
    {
        return a / Magnitude(a);
    }

    public static Vector3 Normalize(Vector3 a)
    {
        return a / Magnitude(a);
    }
    #endregion
}
