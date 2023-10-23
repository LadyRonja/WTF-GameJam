using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMath : MonoBehaviour
{
    public static float Distance(Vector2 vectorA, Vector2 vectorB)
    {
        return Magnitude(vectorA - vectorB);
    }

    public static float Magnitude(Vector2 vector)
    {
        return Hypotenuse(vector.x, vector.y);
    }

    public static float Hypotenuse(float a, float b)
    {
        return 1 / InvSqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
    }

    // John Carmack's legendary algorithm
    public static float InvSqrt(float x)
    {
        float xhalf = 0.5f * x;
        int i = BitConverter.SingleToInt32Bits(x);
        i = 0x5f3759df - (i >> 1);
        x = BitConverter.Int32BitsToSingle(i);
        x *= 1.5f - xhalf * x * x;
        return x;
    }
}
