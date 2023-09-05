using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombiningMatrices : MonoBehaviour
{
    public Vector3 CubeCenter = Vector3.zero;

    public Vector3 CubeSize = Vector3.one;



    public Vector3 Scaling = Vector3.one;

    public Vector3 Rotate = Vector3.zero;

    public Vector3 Translation = Vector3.zero;
    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.red;
        Vector3[] cubePoints = FGizmos.GetCubePoints(CubeCenter, CubeSize);
        FGizmos.DrawWireCube(cubePoints);

        Matrix4x4 scaleMatrix = FMatrix.Sclaing(Scaling);
        Matrix4x4 transMatrix = FMatrix.Translation(Translation);
        Matrix4x4 rotateX = FMatrix.RoateX(Rotate.x);
        Matrix4x4 rotateY = FMatrix.RoateY(Rotate.y);
        Matrix4x4 rotateZ = FMatrix.RoateZ(Rotate.z);
        Matrix4x4 rotateMatrix = rotateZ * rotateY * rotateX;

        for (int i = 0; i < cubePoints.Length; i++)
        {
            Vector3 p = cubePoints[i];
         
            //first scale then rotate last translation
            Matrix4x4 combineMatrix = transMatrix * rotateMatrix * scaleMatrix;
            cubePoints[i] = combineMatrix.MultiplyPoint(p);
        }

        Gizmos.color = Color.green;
        FGizmos.DrawWireCube(cubePoints);
    }
}
