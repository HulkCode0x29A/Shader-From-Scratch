using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MatrixRotation : MonoBehaviour
{
    public enum RotateAxis
    {
        XAxis,
        YAxis,
        ZAxis
    }
    public Vector3 StartPos;

    [Range(0, 360)]
    public float Angle;

    public RotateAxis Axis;

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.red;
        Vector3 pos = StartPos;
        Gizmos.DrawLine(Vector3.zero, pos);
        Gizmos.DrawSphere(StartPos, 0.1f);

        Matrix4x4 matrix;
        if (Axis == RotateAxis.XAxis)
            matrix = FMatrix.RoateX( Angle);
        else if (Axis == RotateAxis.YAxis)
            matrix = FMatrix.RoateY(Angle);
        else
            matrix = FMatrix.RoateZ(Angle);

        Vector3 newPos = matrix.MultiplyPoint(StartPos);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, newPos);
        Gizmos.DrawSphere(newPos, 0.1f);

    }
}
