using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricCoordinates : MonoBehaviour
{
    public FGL gl;

    public Vector2[] Points = new Vector2[3];

    public Color[] Colors = new Color[3] { Color.red, Color.green, Color.blue };



    void Start()
    {
        gl =this.GetComponent<FGL>();
    
    }

    
    void Update()
    {
        gl.Clear();

        //random color
        //float s = Mathf.Sin(Time.time) * 0.5f + 0.5f;
        //float c = Mathf.Cos(Time.time) * 0.5f + 0.5f;
        //float t = s * c;
        //Colors[0] = new Color(s,c,t);
        //Colors[1] = new Color(s,t,c);
        //Colors[2] = new Color(c, s, t);
        gl.DrawTriangle(Points, Colors);
    }
}
