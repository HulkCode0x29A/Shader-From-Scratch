using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorCrossProduct : MonoBehaviour
{
    public GameObject Sphere1;

    public GameObject Sphere2;

    public bool Normalize;

    [Header("Values")]
    public float CrossLength;

    private void OnDrawGizmos()
    {
        if (null == Sphere1 || null == Sphere2)
            return;

        Vector3 p1 = Sphere1.transform.position;
        Vector3 p2 = Sphere2.transform.position;

        if(Normalize)
        {
            p1 = p1.normalized;
            p2 = p2.normalized;
        }
        

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, p1);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, p2);

        float crossx = p1.y * p2.z - p1.z * p2.y;
        float crossy = p1.z * p2.x - p1.x * p2.z;
        float crossz = p1.x * p2.y - p1.y * p2.x;

        //Vector3 p = Vector3.Cross(p1, p2);

        Vector3 p = new Vector3(crossx, crossy, crossz);

        CrossLength = p1.magnitude * p2.magnitude * Mathf.Sin(Vector3.Angle(p1, p2) * Mathf.Deg2Rad);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, p);
    }
}
