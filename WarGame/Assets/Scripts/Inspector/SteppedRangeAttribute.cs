using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class SteppedRangeAttribute : PropertyAttribute
{
    public readonly float min;
    public readonly float max;
    public readonly float step;

    public SteppedRangeAttribute(float min, float max, float step)
    {
        this.min = min;
        this.max = max;
        this.step = step;
    }
}
