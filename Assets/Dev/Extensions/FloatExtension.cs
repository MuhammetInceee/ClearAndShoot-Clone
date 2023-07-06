using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtension
{
    public static float NormalizeMinMax(this float value, float min, float max)
    {
        return Mathf.Clamp01((value - min) / (max - min));
    }

    public static float Normalize(this float value)
    {
        return Mathf.Clamp01(value);
    }
}
