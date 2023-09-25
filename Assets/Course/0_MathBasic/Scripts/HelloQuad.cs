using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloQuad : MonoBehaviour
{
    public Vector3 P0;

    public Vector3 P1;

    public Vector3 P2;

    public Vector3 P3;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(P0, P1);
        Gizmos.DrawLine(P1, P2);
        Gizmos.DrawLine(P2, P3);
        Gizmos.DrawLine(P3, P0);

        //GizmosExtension.DrawQuad(P0,P1,P2,P3);
    }
}
