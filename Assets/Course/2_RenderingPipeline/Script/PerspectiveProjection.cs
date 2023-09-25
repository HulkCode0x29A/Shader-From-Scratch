using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveProjection : MonoBehaviour
{
    [Header("Camera")]
    public float Left = -1;

    public float Right = 1;

    public float Bottom = -1;

    public float Top = 1;

    public float ZNear = 1f;

    public float ZFar = 10f;

    public Vector3[] Points;

    public Color[] Colors;

    [Header("Control")]
    public bool DrawLHCoordinate = true;

    public bool DrawNormalizedCube = true;

    public bool DrawFrustum = true;

   

    private void OnDrawGizmos()
    {
        if(DrawLHCoordinate)
            FGizmos.DrawLHCoordinate(Vector3.zero);

        if(DrawNormalizedCube)
        {
            Gizmos.color = Color.magenta;
            FGizmos.DrawNormalizedCube();
        }
        

        if (null == Points || null == Colors || Points.Length != Colors.Length)
            return;

        FGizmos.DrawWirePolygonWithSphere(Points, Colors, Color.green,0.05f);

        if(DrawFrustum)
        {
            Gizmos.color = Color.green;
            FGizmos.DrawFrustum(Left, Right, Bottom, Top, -ZNear, -ZFar);
        }

        Matrix4x4 perspectiveMatrix = FMatrix.Perspective(Left, Right, Bottom, Top, -ZNear, -ZFar);
        Vector3[] ndcPositions = new Vector3[Points.Length];
        for (int i = 0; i < Points.Length; i++)
        {
            //homogeneous coordinates
            Vector4 homogeneousCoordinates = new Vector4(Points[i].x, Points[i].y, Points[i].z, 1);
            Vector4 clipPos = perspectiveMatrix * homogeneousCoordinates;
            //perspective division
            Vector3 ndcPos = new Vector3(clipPos.x / clipPos.w, clipPos.y / clipPos.w, clipPos.z / clipPos.w);
            ndcPositions[i] = ndcPos;
        }

        FGizmos.DrawWirePolygonWithSphere(ndcPositions, Colors, Color.green,0.05f);
    }
}
