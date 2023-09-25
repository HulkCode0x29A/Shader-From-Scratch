using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Combinedata : MonoBehaviour
{
    public FGL GL;

    int shaderID;
    int vboID;
    void Start()
    {
        TestVertexShader vertexShader = new TestVertexShader();
        TestFragmentShader fragmentShader = new TestFragmentShader();

        shaderID = GL.CreateShader(vertexShader, fragmentShader);
        vboID = GL.GenBuffers();
        GL.BindBuffer(GLBind.GL_ARRAY_BUFFER, vboID);
        GL.BufferData(GLBind.GL_ARRAY_BUFFER, new List<float>() {
            //position                  //color
            -0.5f,-0.5f,0,              1,0,0,
            0.5f,-0.5f,0,               0,1,0,
            0.5f,0.5f,0,                0,0,1,      //first triangle
            0.5f,0.5f,0,                0,0,1,
            -0.5f,0.5f,0,               1,1,0,
            -0.5f,-0.5f,0               ,0,1,1      //second triangle
        });
        GL.VertexAttribPointer(0, 3, 6, 0);
        GL.VertexAttribPointer(1, 3, 6, 3);

        //first use shader
        GL.UseProgram(shaderID);
        //set viewport 
        int resolution = GL.GetScreenResolution();
        Matrix4x4 matrix = FMatrix.ViewPort(0, resolution - 1, 0, resolution  -1);
        GL.SetMatrix("ViewPort", matrix);
    }

    private void OnDrawGizmos()
    {
        
        if (!Application.isPlaying)
            return;

        GL.Clear();
        GL.UseProgram(shaderID);

        GL.BindBuffer(GLBind.GL_ARRAY_BUFFER, vboID);
        GL.DrawArrays(GLDraw.GL_TRIANGLES, 6);
    }

}
