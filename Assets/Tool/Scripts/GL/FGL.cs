using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

public enum GLBind
{
    GL_ARRAY_BUFFER,
}

public enum GLDraw
{
    GL_TRIANGLES
}


public class FGL : MonoBehaviour
{
    public FScreen Screen;

    private List<float> vertices = new List<float>();

    private List<int> colors = new List<int>();

    //state cache

    private int shaderID;
    private int currentShaderID;
    private Dictionary<int, ShaderBase> shaders = new Dictionary<int, ShaderBase>();

    private int vboID;
    private int currentVBOID;
    private Dictionary<int, VertexBufferObject> vbos = new Dictionary<int, VertexBufferObject>();

    void Awake()
    {
        Screen.Init(this);

    }

    /// <summary>
    /// bind shader id
    /// </summary>
    /// <param name="shader"></param>
    /// <returns></returns>
    public int CreateShader(ShaderBase shader)
    {
        shader.ID = shaderID++;
        shaders.Add(shader.ID, shader);
        return shader.ID;
    }

    /// <summary>
    /// clear shaders
    /// </summary>
    public void ClearShader()
    {
        this.shaders.Clear();
    }

    /// <summary>
    /// generate  vertex buffer
    /// </summary>
    /// <returns></returns>
    public int GenBuffers()
    {
        VertexBufferObject vbo = new VertexBufferObject();
        vbo.ID = vboID++;
        this.vbos.Add(vbo.ID, vbo );
        return vbo.ID;
    }

    public void BindBuffer(GLBind bind, int id)
    {
        switch (bind)
        {
            case GLBind.GL_ARRAY_BUFFER:
                this.currentVBOID = id;
                break;
            default:
                Debug.LogError("unknown bind:" + bind);
                break;
        }
    }

    /// <summary>
    /// set buffer data
    /// </summary>
    /// <param name="bind"></param>
    /// <param name="buffers"></param>
    public void BufferData(GLBind bind, List<float> buffers)
    {

        switch (bind)
        {
            case GLBind.GL_ARRAY_BUFFER:
                if (!this.vbos.ContainsKey(currentVBOID))
                    throw new Exception("unknown vboId bind:" + bind +" currentid:" + currentVBOID );

                VertexBufferObject vbo = this.vbos[currentVBOID];
                vbo.SetBuffers(buffers);
                break;
            default:
                Debug.LogError("unknown bind:" + bind);
                break;
        }
    }

    /// <summary>
    /// define how to parse data.
    /// Note that now we only set properties in vertex shaders through this interface
    /// Other data that needs to be interpolated, such as color, is set by matrix use SetMatrix interface
    /// The reason we do this is because we traversed the triangle first, and the data in shader is the data of the last vertex after the triangle traversed
    /// </summary>
    /// <param name="index"></param>
    /// <param name="size"></param>
    /// <param name="stride"></param>
    /// <param name="offset"></param>
    public void VertexAttribPointer(int index, int size, int stride, int offset)
    {
        if (!this.vbos.ContainsKey(currentVBOID))
            throw new Exception("unknown vbo:" + currentVBOID);

        VertexBufferObject vbo = this.vbos[currentVBOID];
        vbo.VertexAttribPointer(index, size, stride, offset);
    }

    /// <summary>
    /// use program or shader
    /// </summary>
    /// <param name="id"></param>
    public void UseProgram(int id)
    {
        this.currentShaderID = id;
    }

    /// <summary>
    /// set shader matrix
    /// </summary>
    /// <param name="shaderID"></param>
    /// <param name="name"></param>
    /// <param name="matrix"></param>
    public void SetMatrix(string name, Matrix4x4 matrix)
    {
        if (!this.shaders.ContainsKey(currentShaderID))
            return;

        ShaderBase shader = this.shaders[currentShaderID];
         FieldInfo field = shader.GetType().GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (null == field || field.FieldType != typeof(Matrix4x4))
            return;

        field.SetValue(shader, matrix);
    }

    public int GetScreenResolution()
    {
        return Screen.Resolution;
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

    /// <summary>
    /// draw vertex according to current data
    /// </summary>
    public void DrawArrays(GLDraw darw, int first, int count)
    {
        //now we only take care of triangles , or you could expand to other sahpe

        ShaderBase shader = shaders[currentShaderID];
        

        VertexBufferObject vbo = vbos[currentVBOID];
        vbo.Reset();
        vbo.SetUpShader(shader);

        //now we only take care of triangles so count must be multiple of 3
        Vector4[] vertexInfo = new Vector4[count];
        for (int i = first; i < count; i++)
        {
            vbo.AnalysisVertexBuffer();
            vertexInfo[i] = shader.Vertex();
        }

        for (int i = 0; i < vertexInfo.Length; i += 3)
        {
            Vector2[] trianglePoints = new Vector2[] { vertexInfo[i], vertexInfo[i + 1], vertexInfo[ i + 2] };
            Screen.DrawTriangle(trianglePoints, shader);
        }

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

    //public void DrawGizmosQuad()
    //{
    //    List<float> quad = GetQuadVertices();

    //    SetVertices(quad);

    //    TriangleData[] datas = Triangularization();
    //    for (int i = 0; i < datas.Length; i++)
    //    {
    //        TriangleData d = datas[i];
    //        FGizmos.DrawWireTriangle(d.p0, d.p1, d.p2);
    //    }
    //}

    //public List<float> GetCubeVertices()
    //{
    //            List<float> vertices = new List<float>() {
    //            -0.5f, -0.5f, -0.5f,
    //             0.5f, -0.5f, -0.5f,
    //             0.5f,  0.5f, -0.5f,
    //             0.5f,  0.5f, -0.5f,
    //            -0.5f,  0.5f, -0.5f,
    //            -0.5f, -0.5f, -0.5f,

    //            -0.5f, -0.5f,  0.5f,
    //             0.5f, -0.5f,  0.5f,
    //             0.5f,  0.5f,  0.5f,
    //             0.5f,  0.5f,  0.5f,
    //            -0.5f,  0.5f,  0.5f,
    //            -0.5f, -0.5f,  0.5f,

    //            -0.5f,  0.5f,  0.5f,
    //            -0.5f,  0.5f, -0.5f,
    //            -0.5f, -0.5f, -0.5f,
    //            -0.5f, -0.5f, -0.5f,
    //            -0.5f, -0.5f,  0.5f,
    //            -0.5f,  0.5f,  0.5f,

    //             0.5f,  0.5f,  0.5f,
    //             0.5f,  0.5f, -0.5f,
    //             0.5f, -0.5f, -0.5f,
    //             0.5f, -0.5f, -0.5f,
    //             0.5f, -0.5f,  0.5f,
    //             0.5f,  0.5f,  0.5f,

    //            -0.5f, -0.5f, -0.5f,
    //             0.5f, -0.5f, -0.5f,
    //             0.5f, -0.5f,  0.5f,
    //             0.5f, -0.5f,  0.5f,
    //            -0.5f, -0.5f,  0.5f,
    //            -0.5f, -0.5f, -0.5f,

    //            -0.5f,  0.5f, -0.5f,
    //             0.5f,  0.5f, -0.5f,
    //             0.5f,  0.5f,  0.5f,
    //             0.5f,  0.5f,  0.5f,
    //            -0.5f,  0.5f,  0.5f,
    //            -0.5f,  0.5f, -0.5f,
    //        };

    //    return vertices;
    //}

    //public void DrawGizmosCube()
    //{
    //    List<float> cube = GetCubeVertices();

    //    SetVertices(cube);

    //    TriangleData[] datas = Triangularization();
    //    for (int i = 0; i < datas.Length; i++)
    //    {
    //        TriangleData d = datas[i];
    //        FGizmos.DrawWireTriangle(d.p0, d.p1, d.p2);
    //    }
    //}

    //private TriangleData[] Triangularization()
    //{
    //    if (null == vertices || vertices.Count == 0)
    //        return new TriangleData[0];

    //    TriangleData[] datas = new TriangleData[vertices.Count / 9];
    //    int dataIndex = 0;
    //    for (int i = 0; i < vertices.Count; i = i + 9)
    //    {
    //        TriangleData d = new TriangleData();
    //        d.p0 = new Vector3(vertices[i], vertices[i + 1], vertices[i + 2]);
    //        d.p1 = new Vector3(vertices[i + 3], vertices[i + 4], vertices[i + 5]);
    //        d.p2 = new Vector3(vertices[i + 6], vertices[i + 7], vertices[i + 8]);
    //        if (colors.Count == vertices.Count)
    //        {
    //            d.c0 = new Color(colors[i], colors[i + 1], colors[i + 2]);
    //            d.c1 = new Color(colors[i + 3], colors[i + 4], colors[i + 5]);
    //            d.c2 = new Color(colors[i + 6], colors[i + 7], colors[i + 8]);
    //        }


    //        datas[dataIndex] = d;
    //        dataIndex++;
    //    }

    //    return datas;
    //}

    private void OnDrawGizmos()
    {
       
    }
}
