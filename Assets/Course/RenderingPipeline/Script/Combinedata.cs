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
        TestShader testShader = new TestShader();

        //VertexBufferObject vbo = new VertexBufferObject();
        //vbo.SetBuffers(new List<float>() { 
        //    -0.5f, 0.5f,0,     
        //    0.5f,0.5f,0,        
        //    0,0.5f,0 //a triangle
        //});

        //vbo.VertexAttribPointer(0, 3, 3, 0);

        // vbo.AnalysisVertexBuffer(testShader);

        shaderID = GL.CreateShader(testShader);
        vboID = GL.GenBuffers();
        GL.BindBuffer(GLBind.GL_ARRAY_BUFFER, vboID);
        GL.BufferData(GLBind.GL_ARRAY_BUFFER, new List<float>() {
                //position      
                -0.5f, 0f, 0,     
                0.5f, 0f, 0,      
                0, 0.5f, 0,       
        });
        GL.VertexAttribPointer(0, 3, 3, 0);


        Matrix4x4 colorMatrix = Matrix4x4.identity;
        colorMatrix.SetRow(0, new Vector4(1,0,0,0));
        colorMatrix.SetRow(1, new Vector4(0,1,0,0));
        colorMatrix.SetRow(2, new Vector4(0,0,1,0));


        //first use shader
        GL.UseProgram(shaderID);
        //set viewport 
        int resolution = GL.GetScreenResolution();
        Matrix4x4 matrix = FMatrix.ViewPort(0, resolution - 1, 0, resolution  -1);
        GL.SetMatrix("ViewPort", matrix);
        GL.SetMatrix("Color", colorMatrix);
    }

    private void OnDrawGizmos()
    {
        
        if (!Application.isPlaying)
            return;

        GL.Clear();
        GL.UseProgram(shaderID);

        GL.BindBuffer(GLBind.GL_ARRAY_BUFFER, vboID);
        GL.DrawArrays(GLDraw.GL_TRIANGLES, 0, 3);
    }

}
