using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVertexShader : ShaderVertexBase
{
    [VertexLayout(0), V2F]
    private Vector3 position;

    [VertexLayout(1), V2F]
    private Vector3 color;

    Matrix4x4 ViewPort;

    public override Vector4 Vertex()
    {
        return ViewPort *  new Vector4(position.x, position.y, position.z, 1);
        
    }

   

}
