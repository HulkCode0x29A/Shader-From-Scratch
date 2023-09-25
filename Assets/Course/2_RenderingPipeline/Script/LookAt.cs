using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [Header("Camera")]
    public float Left = -1;

    public float Right = 1;

    public float Bottom = -1;

    public float Top = 1;

    public float ZNear = 1f;

    public float ZFar = 10f;

    [Header("LookAt")]

    public Transform Target;

    public Transform CopyTarget;

    public Transform VirtualCamera;

    public Transform CopyCamera;

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        if (null == Target || null == CopyTarget || null == VirtualCamera || null == CopyCamera)
            return;

        Vector3 camPos = VirtualCamera.position;
        Vector3 targetPos = Target.position;
        //set up virtual camera
        Vector3 viewZ = camPos - targetPos;
        viewZ = viewZ.normalized;

        //draw view z axis
        Gizmos.color = Color.blue;
        VirtualCamera.forward = -viewZ;
        Gizmos.DrawLine(camPos, camPos + viewZ);

        Gizmos.color = Color.green;
        Vector3 viewY = new Vector3(0, 1, 0);
        //Gizmos.DrawLine(camPos, camPos + viewY);

        //draw view x axis
        Gizmos.color = Color.red;
        Vector3 viewX = Vector3.Cross(viewY, viewZ);
        viewX = viewX.normalized;
        Gizmos.DrawLine(camPos, camPos + viewX);

        //adjust view y axis
        Gizmos.color = Color.green;
        viewY = Vector3.Cross(viewZ, viewX);
        Gizmos.DrawLine(camPos, camPos + viewY);

        Gizmos.color = Color.red;
        FGizmos.DrawFrustum(Left, Right, Bottom, Top, ZNear, ZFar, VirtualCamera);

        //set copy obj to same position and same rotation
        CopyTarget.position = Target.position;
        CopyTarget.rotation = Target.rotation;
        CopyCamera.position = VirtualCamera.position;
        CopyCamera.rotation = VirtualCamera.rotation;

        //fist move the camera to the origin do the same for the CopyTarget
        Matrix4x4 transMatrix = FMatrix.Translation(-camPos);//pass in the negative camera position
        Vector3 newCamPos = transMatrix.MultiplyPoint(camPos);
        Vector3 newTargetPos = transMatrix.MultiplyPoint(targetPos);

        Matrix4x4 lookAtMatrix = FMatrix.Lookat(camPos, targetPos, Vector3.up);
        Vector3 viewSpaceTargetPos = lookAtMatrix.MultiplyPoint(newTargetPos);

      
        Matrix4x4 viewCoordinateMatrix = Matrix4x4.identity;
        //set viewX to first column
        viewCoordinateMatrix[0, 0] = viewX.x;
        viewCoordinateMatrix[1, 0] = viewX.y;
        viewCoordinateMatrix[2, 0] = viewX.z;
        //set viewY to second column
        viewCoordinateMatrix[0, 1] = viewY.x;
        viewCoordinateMatrix[1, 1] = viewY.y;
        viewCoordinateMatrix[2, 1] = viewY.z;
        //set viewZ to third column
        viewCoordinateMatrix[0, 2] = viewZ.x;
        viewCoordinateMatrix[1, 2] = viewZ.y;
        viewCoordinateMatrix[2, 2] = viewZ.z;

        Matrix4x4 rotateViewCoordinate = lookAtMatrix * viewCoordinateMatrix;

        CopyCamera.position = newCamPos;
       
        CopyCamera.forward = -rotateViewCoordinate.GetColumn(2);
        CopyTarget.position = viewSpaceTargetPos;

        Gizmos.color = Color.green;
        FGizmos.DrawFrustum(Left, Right, Bottom, Top, ZNear, ZFar, CopyCamera);

    }
}
