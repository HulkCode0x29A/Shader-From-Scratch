using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCube : MonoBehaviour
{
    public Vector3 Size;

    public Vector3 Center;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3[] points = new Vector3[8];
        float halfx = Size.x / 2;
        float halfy = Size.y / 2;
        float halfz = Size.z / 2;
        //Counterclockwise one face
        points[0] = new Vector3(Center.x + halfx, Center.y + halfy, Center.z + halfz);//z is fixed, top right corner
        points[1] = new Vector3(Center.x - halfx, Center.y + halfy, Center.z + halfz);//z is fixed, top left corner
        points[2] = new Vector3(Center.x - halfx, Center.y - halfy, Center.z + halfz);//z is fixed, bottom left corner
        points[3] = new Vector3(Center.x + halfx, Center.y - halfy, Center.z + halfz);//z is fixed, bottom right corner
        //Counterclockwise opposite face
        points[4] = new Vector3(Center.x + halfx, Center.y + halfy, Center.z - halfz);
        points[5] = new Vector3(Center.x - halfx, Center.y + halfy, Center.z - halfz);
        points[6] = new Vector3(Center.x - halfx, Center.y - halfy, Center.z - halfz);
        points[7] = new Vector3(Center.x + halfx, Center.y - halfy, Center.z - halfz);

        //one face
        Gizmos.DrawLine(points[0], points[1]);
        Gizmos.DrawLine(points[1], points[2]);
        Gizmos.DrawLine(points[2], points[3]);
        Gizmos.DrawLine(points[3], points[0]);

        //another face
        Gizmos.DrawLine(points[4], points[5]);
        Gizmos.DrawLine(points[5], points[6]);
        Gizmos.DrawLine(points[6], points[7]);
        Gizmos.DrawLine(points[7], points[4]);

        //connect two faces
        Gizmos.DrawLine(points[0], points[4]);
        Gizmos.DrawLine(points[1], points[5]);
        Gizmos.DrawLine(points[2], points[6]);
        Gizmos.DrawLine(points[3], points[7]);

        //Gizmos.color = Color.red;
        //GizmosExtension.DrawWireCube(Center, Size);
    }
}
