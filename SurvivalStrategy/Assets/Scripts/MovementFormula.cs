using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFormula
{
    //ParabolicArc returns us a height value given the height of the vertex and the distance from the final location
    public static float parabolicArc(float height, float distance, float x)
    {
        float mid_point = distance / 2;
        float a = (-height)/Mathf.Pow(((-distance)/2), 2);        //'a' represents the variable 'a' in the quadratic function
        return a * Mathf.Pow((x - height), 2) + distance / 2;
    }
}
