using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorLength : MonoBehaviour
{
    public GameObject Sphere;

    [Header("Length")]
    public float Length;

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        if(null != Sphere)
        {
            Gizmos.color = Color.green;
            FGizmos.DrawLineWithArrow(Vector3.zero, Sphere.transform.position, 0.25f);

            Length = GetLength(Sphere.transform.position);
        }
    }

    private float GetLength(Vector3 v)
    {
        float l = Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        //float l = v.magnitude;
        return l;
    }
}
