using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDotProduct : MonoBehaviour
{
    public GameObject Sphere1;

    public GameObject Sphere2;

    public bool Normalize;

    [Header("Values")]
    public float DotResult;

    public float Theta;

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        if (null == Sphere1 || null == Sphere2)
            return;

        Vector3 p1 = Sphere1.transform.position;
        Vector3 p2 = Sphere2.transform.position;

        Gizmos.color = Color.green;
        FGizmos.DrawLineWithArrow(Vector3.zero, p1, 0.25f);

        Gizmos.color = Color.red;
        FGizmos.DrawLineWithArrow(Vector3.zero, p2, 0.25f);

        if(Normalize)
        {
            p1 = p1.normalized;
            p2 = p2.normalized;

            Gizmos.color = Color.yellow;
            FGizmos.DrawLineWithArrow(Vector3.zero, p1, 0.25f);
            FGizmos.DrawLineWithArrow(Vector3.zero, p2, 0.25f);
        }

        //DotResult = Vector3.Dot(p1, p2);

        //DotResult = p1.x * p2.x + p1.y * p2.y + p1.z * p2.z;

        DotResult = p1.magnitude * p2.magnitude * Mathf.Cos(Vector3.Angle(p1, p2) * Mathf.Deg2Rad);

        float numerator = Vector3.Dot(p1, p2);
        float denominator = p1.magnitude * p2.magnitude;
        Theta = Mathf.Acos((numerator / denominator) ) * Mathf.Rad2Deg;
    }
}
