using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayHelper 
{
    public static List<float> Vector3ToArray(Vector3[] list)
    {
        List<float> floats = new List<float>();
        for (int i = 0; i < list.Length; i++)
        {
            floats.Add(list[i].x);
            floats.Add(list[i].y);
            floats.Add(list[i].z);
        }

        return floats;
    }
}
