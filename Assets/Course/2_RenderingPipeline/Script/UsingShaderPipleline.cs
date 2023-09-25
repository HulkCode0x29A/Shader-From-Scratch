using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingShaderPipleline : MonoBehaviour
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

    public float RotateSpeed = 1f;

    private float rotateZ = 0;

    [Header("Camera")]

    public Transform VirtualCamera;

    [Header("GL")]

    public FGL GL;

    int shaderID;

    int vboID;


    [Header("Debug")]
    public Vector3 D0;

    void Start()
    {
        PiplelineVertexShader vertexShader = new PiplelineVertexShader();
        PiplelineFragmentShader fragmentShader = new PiplelineFragmentShader();

        shaderID = GL.CreateShader(vertexShader, fragmentShader);
        vboID = GL.GenBuffers();
        GL.BindBuffer(GLBind.GL_ARRAY_BUFFER, vboID);

        GL.VertexAttribPointer(0,3,6,0);
        GL.VertexAttribPointer(1,3,6,3);


      
    }

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        if (null == VirtualCamera )
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

        rotateZ += Time.deltaTime * RotateSpeed;
        // We can translate, rotate, or scale into this matrix
        Matrix4x4 rotateMtarix = FMatrix.RoateZ(rotateZ);
        float scale = 1f + (Mathf.Sin(Time.realtimeSinceStartup) * 0.5f + 0.5f);
        Matrix4x4 scaleMatrix = FMatrix.Scaling(new Vector3(scale, scale, scale));
        Matrix4x4 modelMatrix = rotateMtarix * scaleMatrix;


        //Convert coordinates to the light source space
        Matrix4x4 translationMatrix = FMatrix.Translation(-VirtualCamera.position);
        Matrix4x4 rotateMatrix = FMatrix.Lookat(camPos, triangleCenter, Vector3.up);
        Matrix4x4 viewMatrix = rotateMatrix * translationMatrix;

        //Perspective division is applied after the coordinates are converted to the clipping space
        int resolution = GL.GetScreenResolution();
        Matrix4x4 perspectiveMatrix = FMatrix.Perspective(Left, Right, Bottom, Top, -ZNear, -ZFar);

        //Convert coordinates to screen space
        Matrix4x4 viewPortMatrix = FMatrix.ViewPort(0, resolution - 1, 0, resolution - 1);

        //Matrix4x4 worldToClip = perspectiveMatrix * viewMatrix * modelMatrix;
        //Vector4 temp = worldToClip * new Vector4(P0.x, P0.y, P0.z, 1);
        //D0 = new Vector3(temp.x / temp.w, temp.y / temp.w, temp.z/temp.w);
        //D0 = viewPortMatrix * new Vector4(D0.x, D0.y,D0.z, 1);

        if (!Application.isPlaying)
            return;

        GL.Clear();
        GL.UseProgram(shaderID);
        GL.BindBuffer(GLBind.GL_ARRAY_BUFFER, vboID);

        GL.BufferData(GLBind.GL_ARRAY_BUFFER, new List<float>() {
            P0.x, P0.y,P0.z,    C0.r, C0.g, C0.b,
            P1.x, P1.y,P1.z,    C1.r, C1.g, C1.b,
            P2.x, P2.y,P2.z,    C2.r, C2.g, C2.b,
        });
      
        GL.SetMatrix("Model", modelMatrix);
        
        GL.SetMatrix("View", viewMatrix);
      
        GL.SetMatrix("Projection", perspectiveMatrix);

        GL.SetMatrix("ViewPort", viewPortMatrix);   

        GL.DrawArrays(GLDraw.GL_TRIANGLES, 3);  
    }

}
