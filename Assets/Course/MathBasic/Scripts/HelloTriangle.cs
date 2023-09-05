using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloTriangle : MonoBehaviour
{
    public Vector3 P0;

    public Vector3 P1;

    public Vector3 P2;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(P0, P1);
        Gizmos.DrawLine(P1, P2);
        Gizmos.DrawLine(P2, P0);

        //GizmosExtension.DrawWireTriangle(P0, P1, P2);
    }
}
