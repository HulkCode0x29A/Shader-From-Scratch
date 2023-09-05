using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FScreen : MonoBehaviour
{
    public GameObject Pixel;

    public Vector3 PixelScale = Vector3.one;

    [Range(10, 50)]
    public int Resolution;

    [Range(0,1)]
    public float Spacing;

    private GameObject[][] pixels;

    private MeshRenderer[][] meshRenderers;
    void Start()
    {
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
                copyPixel.transform.localPosition = new Vector3(x + Spacing * x, y + Spacing * y, 0);

                pixels[y][x] = copyPixel;
                meshRenderers[y][x] = copyPixel.GetComponent<MeshRenderer>();
            }
        }

        
    }

    public void SetColor(int x, int y, Color color)
    {
        meshRenderers[y][x].material.color = color;
    }

    private void Update()
    {
       
    }

}
