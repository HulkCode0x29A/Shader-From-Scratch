using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TriangleData
{
    public Vector3 p0;

    public Vector3 p1;

    public Vector3 p2;

    public Color c0;

    public Color c1;

    public Color c2;
}

public class FGL : MonoBehaviour
{
    public FScreen Screen;

    private List<float> vertices = new List<float>();

    private List<int> colors = new List<int>();

    void Awake()
    {
        Screen.Init(this);

    }

    public void SetVertices(List<float> vertices)
    {
        this.vertices = vertices;
    }

    public void SetColors(List<int> colors)
    {
        this.colors = colors;
    }

    public void SetClearColor(Color clearColor)
    {
        Screen.ClearColor = clearColor;
    }

    public void Clear()
    {
        Screen.Clear();
    }

    public void DrawLine(Vector2 p0, Vector2 p1, Color c0, Color c1)
    {
        Screen.DrawLine(p0, p1, c0, c1);
    }

    public void DrawTriangle(Vector2[] points, Color[] colors)
    {
        Screen.DrawTriangle(points, colors);
    }


    public static Vector3 Barycentric(Vector2[] points, Vector2 p)
    {
        if (null == points || points.Length != 3)
        {
            Debug.LogError("Error data for Barycentric");
            return Vector3.one;
        }

        //alculate the area of a triangle using the determinant
        float area = points[0].x * points[1].y + points[1].x * points[2].y + points[2].x * points[0].y
        - points[2].x * points[1].y - points[1].x * points[0].y - points[0].x * points[2].y;

        //sub triangle area
        float subArea0 = p.x * points[1].y + points[1].x * points[2].y + points[2].x * p.y
        - points[2].x * points[1].y - points[1].x * p.y - p.x * points[2].y;

        float subArea1 = points[0].x * p.y + p.x * points[2].y + points[2].x * points[0].y
        - points[2].x * p.y - p.x * points[0].y - points[0].x * points[2].y;
        //calculating barycentric coordinates
        float alpha = subArea0 / area;
        float beta = subArea1 / area;
        float gamma = 1 - alpha - beta;

        return new Vector3(alpha, beta, gamma);
    }

    public List<float> GetQuadVertices()
    {
        //define a quad vertices
        List<float> quad = new List<float>()
        {
            -0.5f,-0.5f,0,    0.5f,-0.5f,0,     0.5f,0.5f,0,//first triangle
            0.5f,0.5f,0,       -0.5f,0.5f,0,    -0.5f,-0.5f,0//second triangle
        };
        return quad;
    }

    public void DrawGizmosQuad()
    {
        List<float> quad = GetQuadVertices();

        SetVertices(quad);

        TriangleData[] datas = Triangularization();
        for (int i = 0; i < datas.Length; i++)
        {
            TriangleData d = datas[i];
            FGizmos.DrawWireTriangle(d.p0, d.p1, d.p2);
        }
    }

    public List<float> GetCubeVertices()
    {
                List<float> vertices = new List<float>() {
                -0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f, -0.5f,
                 0.5f,  0.5f, -0.5f,
                 0.5f,  0.5f, -0.5f,
                -0.5f,  0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,

                -0.5f, -0.5f,  0.5f,
                 0.5f, -0.5f,  0.5f,
                 0.5f,  0.5f,  0.5f,
                 0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f,  0.5f,
                -0.5f, -0.5f,  0.5f,

                -0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f,
                -0.5f, -0.5f,  0.5f,
                -0.5f,  0.5f,  0.5f,

                 0.5f,  0.5f,  0.5f,
                 0.5f,  0.5f, -0.5f,
                 0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f,  0.5f,
                 0.5f,  0.5f,  0.5f,

                -0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f, -0.5f,
                 0.5f, -0.5f,  0.5f,
                 0.5f, -0.5f,  0.5f,
                -0.5f, -0.5f,  0.5f,
                -0.5f, -0.5f, -0.5f,

                -0.5f,  0.5f, -0.5f,
                 0.5f,  0.5f, -0.5f,
                 0.5f,  0.5f,  0.5f,
                 0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f,  0.5f,
                -0.5f,  0.5f, -0.5f,
            };

        return vertices;
    }

    public void DrawGizmosCube()
    {
        List<float> cube = GetCubeVertices();

        SetVertices(cube);

        TriangleData[] datas = Triangularization();
        for (int i = 0; i < datas.Length; i++)
        {
            TriangleData d = datas[i];
            FGizmos.DrawWireTriangle(d.p0, d.p1, d.p2);
        }
    }

    private TriangleData[] Triangularization()
    {
        if (null == vertices || vertices.Count == 0)
            return new TriangleData[0];

        TriangleData[] datas = new TriangleData[vertices.Count / 9];
        int dataIndex = 0;
        for (int i = 0; i < vertices.Count; i = i + 9)
        {
            TriangleData d = new TriangleData();
            d.p0 = new Vector3(vertices[i], vertices[i + 1], vertices[i + 2]);
            d.p1 = new Vector3(vertices[i + 3], vertices[i + 4], vertices[i + 5]);
            d.p2 = new Vector3(vertices[i + 6], vertices[i + 7], vertices[i + 8]);
            if (colors.Count == vertices.Count)
            {
                d.c0 = new Color(colors[i], colors[i + 1], colors[i + 2]);
                d.c1 = new Color(colors[i + 3], colors[i + 4], colors[i + 5]);
                d.c2 = new Color(colors[i + 6], colors[i + 7], colors[i + 8]);
            }


            datas[dataIndex] = d;
            dataIndex++;
        }

        return datas;

    }

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);
        Gizmos.color = Color.white;
        DrawGizmosCube();
    }
}
