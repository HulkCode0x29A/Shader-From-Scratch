using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpace : MonoBehaviour
{
    public FScreen Screen;

    public Vector3[] Points = new Vector3[3];

    public Color[] Colors = new Color[3];

    public Vector2 StartPos;

    public Vector2 End;

    private Matrix4x4 viewportMatrix;

    void Start()
    {
         viewportMatrix = FMatrix.ViewPort(0, Screen.Resolution - 1, 0, Screen.Resolution - 1);
        

        

    }

    void Update()
    {
        Screen.Clear();

        Vector3[] screenPos = new Vector3[3];
        for (int i = 0; i < Points.Length; i++)
        {
            screenPos[i] = viewportMatrix.MultiplyPoint(Points[i]);
        }

        //color visual
        //Screen.DrawLine(screenPos[0], screenPos[1], Colors[0], Colors[1]);
        //Screen.DrawLine(screenPos[1], screenPos[2], Colors[1], Colors[2]);
        //Screen.DrawLine(screenPos[2], screenPos[0], Colors[2], Colors[0]);

        //depth visual
        Screen.ClearColor = Color.green;
        Screen.Clear();
        float z0 = screenPos[0].z;
        float z1 = screenPos[1].z;
        float z2 = screenPos[2].z;
        Screen.DrawLine(screenPos[0], screenPos[1], new Color(z0, z0,z0), new Color(z1, z1, z1));
        Screen.DrawLine(screenPos[1], screenPos[2], new Color(z1, z1, z1), new Color(z2, z2, z2));
        Screen.DrawLine(screenPos[2], screenPos[0], new Color(z2, z2, z2), new Color(z0, z0, z0));
    }

    private void OnDrawGizmos()
    {
      
        Gizmos.color = Color.magenta;
        FGizmos.DrawNormalizedCube();

        if (null == Points || null == Colors || Points.Length != Colors.Length)
            return;

        FGizmos.DrawWirePolygonWithSphere(Points, Colors, Color.green, 0.05f);
    }
}
