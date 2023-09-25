using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFrustum : MonoBehaviour
{
    public float Left = -1;

    public float Right = 1;

    public float Bottom = -1;

    public float Top = 1;

    public float ZNear = 1f;

    public float ZFar = 10f;

    public GameObject VirtualCamera;
    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        if (null == VirtualCamera)
            return;

        Gizmos.color = Color.green;
        FrustumData data = FGizmos.GetFrustumData(Left, Right, Bottom, Top, ZNear, ZFar);

        Matrix4x4 transMatrix = FMatrix.Translation(VirtualCamera.transform.position);
        Vector3 eulerAngle = VirtualCamera.transform.eulerAngles;
        Matrix4x4 rotateX = FMatrix.RoateX(eulerAngle.x);
        Matrix4x4 rotateY = FMatrix.RoateY(eulerAngle.y);
        Matrix4x4 rotateZ = FMatrix.RoateZ(eulerAngle.z);
        Matrix4x4 rotateMatrix = rotateZ * rotateY * rotateX;

        data.Transformation(transMatrix * rotateMatrix);
        FGizmos.DrawFrustum(data);
        
    }
}
