using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FScreen :MonoBehaviour
{
    public GameObject Pixel;

    public Vector3 PixelScale = Vector3.one;

    private FGL gl;

 

    [Range(10, 50)]
    public int Resolution;

    public Color ClearColor { get; set; } = Color.white;

    [Range(0, 1)]
    public float Spacing;

    private GameObject[][] pixels;

    private MeshRenderer[][] meshRenderers;


    public void Init(FGL gl)
    {
        this.gl = gl;

        pixels = new GameObject[Resolution][];
        meshRenderers = new MeshRenderer[Resolution][];
        for (int y = 0; y < Resolution; y++)
        {
            pixels[y] = new GameObject[Resolution];
            meshRenderers[y] = new MeshRenderer[Resolution];

            for (int x = 0; x < Resolution; x++)
            {
                GameObject copyPixel = GameObject.Instantiate(Pixel);
                copyPixel.transform.SetParent(this.transform);
                copyPixel.transform.localScale = PixelScale;
                copyPixel.transform.localPosition = new Vector3(x * PixelScale.x + Spacing * x, y * PixelScale.y + Spacing * y, 0);
                copyPixel.name = "Pixel_" + y + "_" + x;

                pixels[y][x] = copyPixel;
                meshRenderers[y][x] = copyPixel.GetComponent<MeshRenderer>();
            }
        }
    }

 

    public void DrawLine(Vector2 p0, Vector2 p1, Color c0, Color c1)
    {
        bool steep = false;
        float x0 = p0.x;
        float y0 = p0.y;
        float x1 = p1.x;
        float y1 = p1.y;
        if (Mathf.Abs(p0.x - p1.x) < Mathf.Abs(p0.y - p1.y)) //if line is steep, transpose 
        {
            Swap(ref x0, ref y0);
            Swap(ref x1, ref y1);
            steep = true;
        }

        if (x0 > x1) // make it left to right
        {
            Swap(ref x0, ref x1);
            Swap(ref y0, ref y1);
        }

        for (int x = (int)x0; x < x1; x++)
        {
            float t = (x - x0) / (float)(x1 - x0);
            int y = (int)(y0 * (1.0f - t) + y1 * t);
            Color color = (c0 * (1.0f - t)) + c1 * t;
            if (steep)
                SetColor(y, x, color);
            else
                SetColor(x, y, color);
        }

    }

    public void DrawTriangle(Vector2[] points, Color[] colors)
    {
        Vector2 boxMin = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 boxMax = new Vector2(-float.MaxValue, -float.MaxValue);
        Vector2 clamp = new Vector2(Resolution - 1, Resolution - 1);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                //bottom-left
                boxMin[j] = Mathf.Max(0, Mathf.Min(boxMin[j], points[i][j]));
                //top-right
                boxMax[j] = Mathf.Min(clamp[j], Mathf.Max(boxMax[j], points[i][j]));
            }
        }

        Vector3 p = Vector3.zero;
        for (p.x = boxMin.x; p.x < boxMax.x; p.x++)
        {
            for (p.y = boxMin.y; p.y < boxMax.y; p.y++)
            {

                Vector3 bcCoord = FGL.Barycentric(points, p);
                //determine if the pixel is in a triangle
                if (bcCoord.x >= 0 && bcCoord.y >= 0 && bcCoord.x + bcCoord.y <= 1)
                {
                    Color interpColor = bcCoord.x * colors[0] + bcCoord.y * colors[1] + bcCoord.z * colors[2];

                    SetColor((int)p.x, (int)p.y, interpColor);
                }
            }
        }

    }

    private void Swap(ref float f1, ref float f2)
    {
        float temp = f1;
        f1 = f2;
        f2 = temp;
    }

    private void Swap(ref Vector2 v1, ref Vector2 v2)
    {
        Vector2 temp = v1;
        v1 = v2;
        v2 = temp;
    }

    public void Clear()
    {
        for (int x = 0; x < Resolution; x++)
        {
            for (int y = 0; y < Resolution; y++)
            {
                SetColor(x, y, ClearColor);
            }
        }
    }

    public void SetColor(int x, int y, Color color)
    {
        if (x >= Resolution || y >= Resolution || x < 0 || y < 0)
            return;

        meshRenderers[y][x].material.color = color; 
    }

}
