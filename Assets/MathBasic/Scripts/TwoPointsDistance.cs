using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointsDistance : MonoBehaviour
{
    public GameObject Sphere1;

    public GameObject Sphere2;

    [Header("Distance")]
    public float Distance;

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);

        if (null == Sphere1 || null == Sphere2)
            return;

        Vector3 p1 = Sphere1.transform.position;
        Vector3 p2 = Sphere2.transform.position;
        float d2 = Mathf.Pow(p1.x - p2.x, 2) + Mathf.Pow(p1.y - p2.y,2) +Mathf.Pow(p1.z - p2.z,2);
        Distance = Mathf.Sqrt(d2);
        //Distance = Vector3.Distance(p1, p2);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(p1, p2);
    }
}
