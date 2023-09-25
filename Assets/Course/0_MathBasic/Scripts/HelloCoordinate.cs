using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloCoordinate : MonoBehaviour
{
    public Vector3 center;
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(Vector3.zero ,  new Vector3(1, 0, 0));
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(Vector3.zero, new Vector3(0, 0, 1));
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(Vector3.zero,  new Vector3(0, 1, 0));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(center, center + new Vector3(1, 0, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(center, center + new Vector3(0, 0, 1));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(center, center + new Vector3(0, 1, 0));

        //GizmosExtension.DrawLHCoordinate(center);
    }
}
