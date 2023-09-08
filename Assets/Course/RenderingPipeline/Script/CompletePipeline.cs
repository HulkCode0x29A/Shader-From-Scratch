using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletePipeline : MonoBehaviour
{
    [Header("Camera")]
    public float Left = -1;

    public float Right = 1;

    public float Bottom = -1;

    public float Top = 1;

    public float ZNear = 1f;

    public float ZFar = 10f;

    [Header("Traingle")]
    public Vector3 P0;

    public Vector3 P1;

    public Vector3 P2;

    public Color C0 = Color.red;

    public Color C1 = Color.green;

    public Color C2 = Color.blue;

    [Header("Camera")]

    public Transform VirtualCamera;

    public Transform CopyCamera;

    [Header("GL")]

    public FGL GL;

    [Header("Control")]

    public bool DrawWorldCoordinate = true;

    

    private void OnDrawGizmos()
    {
        if(DrawWorldCoordinate)
            FGizmos.DrawLHCoordinate(Vector3.zero);

        if (null == VirtualCamera || null == CopyCamera)
            return;

        FGizmos.DrawWirePolygonWithSphere(new Vector3[] { P0, P1, P2 }, new Color[] { C0, C1, C2 }, Color.green, 0.1f);

        Gizmos.color = Color.red;
        FGizmos.DrawFrustum(Left, Right, Bottom, Top, -ZNear, -ZFar, VirtualCamera);

        //computed triangle center
        Gizmos.color = Color.yellow;
        Vector3 triangleCenter = (P0 + P1 + P2) / 3;
        Gizmos.DrawSphere(triangleCenter, 0.1f);

        Vector3 camPos = VirtualCamera.position;
        Vector3 cameraForward = camPos - triangleCenter;
        VirtualCamera.forward = cameraForward;

        //Next transform the coordinates to the camera coordinate system
        //You can change it in gizmos or you can change it in shader, 
        //and as a demonstration we're going to transform it in gizmos
        //like 1_LookAt chapter

        //move cameras and objects to the center of the world
        DrawViewAxis(camPos, triangleCenter);//

        Matrix4x4 translationMatrix = FMatrix.Translation(-VirtualCamera.position);
        Matrix4x4 rotateMatrix = FMatrix.Lookat(camPos, triangleCenter, Vector3.up);
        Matrix4x4 viewMatrix = rotateMatrix * translationMatrix;
        Vector3 newCamPos = viewMatrix.MultiplyPoint(camPos);
        CopyCamera.position = newCamPos;

        //We already know that the result of the transformation is to rotate the camera coordinates to
        //align with the world coordinate system, so this can be set directly here
        CopyCamera.forward = Vector3.forward;
        Gizmos.color = Color.green;
        FGizmos.DrawFrustum(Left, Right, Bottom, Top, -ZNear, -ZFar, CopyCamera);

        //darw transformed triangle
        Vector3 t0 = viewMatrix.MultiplyPoint(P0);
        Vector3 t1 = viewMatrix.MultiplyPoint(P1);
        Vector3 t2 = viewMatrix.MultiplyPoint(P2);
        FGizmos.DrawWirePolygonWithSphere(new Vector3[] { t0, t1, t2 }, new Color[] { C0, C1, C2 }, Color.green, 0.1f);
        Gizmos.color = Color.yellow;
        triangleCenter = (t0 + t1 + t2) / 3;
        Gizmos.DrawSphere(triangleCenter, 0.1f);

        //draw normalized cube
        Gizmos.color = Color.magenta;
        FGizmos.DrawNormalizedCube();

        Matrix4x4 perspectiveMatrix = FMatrix.Perspective(Left, Right, Bottom,Top,  -ZNear, -ZFar);
        Vector4 clipv0 = perspectiveMatrix * new Vector4(t0.x, t0.y, t0.z, 1);
        Vector4 clipv1 = perspectiveMatrix * new Vector4(t1.x, t1.y, t1.z, 1);
        Vector4 clipv2 = perspectiveMatrix * new Vector4(t2.x, t2.y, t2.z, 1);
        //perspective division
        Vector3 ndcv0 = new Vector3(clipv0.x / clipv0.w, clipv0.y / clipv0.w, clipv0.z /clipv0.w);
        Vector3 ndcv1 = new Vector3(clipv1.x / clipv1.w, clipv1.y / clipv1.w, clipv1.z / clipv1.w);
        Vector3 ndcv2 = new Vector3(clipv2.x / clipv2.w, clipv2.y / clipv2.w, clipv2.z / clipv2.w);

        //draw ndc triangle
        FGizmos.DrawWirePolygonWithSphere(new Vector3[] { ndcv0, ndcv1, ndcv2}, new Color[] { C0, C1, C2},Color.green);

        int resolution = GL.GetScreenResolution();
        Matrix4x4 viewportMatrix = FMatrix.ViewPort(0, resolution - 1, 0, resolution - 1);
    }



    private void DrawViewAxis(Vector3 camPos, Vector3 targetPos)
    {

        //set up virtual camera
        Vector3 viewZ = camPos - targetPos;
        viewZ = viewZ.normalized;

        //draw view z axis
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(camPos, camPos + viewZ);

        //Gizmos.color = Color.green;
        Vector3 viewY = new Vector3(0, 1, 0);

        ////draw view x axis
        Gizmos.color = Color.red;
        Vector3 viewX = Vector3.Cross(viewY, viewZ);
        viewX = viewX.normalized;
        Gizmos.DrawLine(camPos, camPos + viewX);

        ////adjust view y axis
        Gizmos.color = Color.green;
        viewY = Vector3.Cross(viewZ, viewX);
        Gizmos.DrawLine(camPos, camPos + viewY);

    }
}
