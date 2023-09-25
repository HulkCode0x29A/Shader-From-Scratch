using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixScaling : MonoBehaviour
{
    public Vector3 P1 = new Vector3(1, 1, 1);

    public Vector3 P2 = new Vector3(0, 1, 1);

    public Vector3 P3 = new Vector3(0.5f, 2, 1);

    public Vector3 Scale = Vector3.one;

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.red;
        FGizmos.DrawWireTriangle(P1, P2, P3);

        Matrix4x4 matrix = FMatrix.Scaling(Scale);

        Gizmos.color = Color.green;
        Vector3 t1 = matrix.MultiplyPoint(P1);
        Vector3 t2 = matrix.MultiplyPoint(P2);
        Vector3 t3 = matrix.MultiplyPoint(P3);
        FGizmos.DrawWireTriangle(t1, t2, t3);
    }
}
