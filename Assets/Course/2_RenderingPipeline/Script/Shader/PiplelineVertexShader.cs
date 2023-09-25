using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiplelineVertexShader : ShaderVertexBase
{
    [VertexLayout(0), V2F]
    private Vector3 position;

    [VertexLayout(1), V2F]
    private Vector3 color;

    private Matrix4x4 Model;

    private Matrix4x4 View; 

    private Matrix4x4 Projection;

    private Matrix4x4 ViewPort;

    public override Vector4 Vertex()
    {
        Vector4 clipPos = Projection * View * Model * new Vector4(position.x, position.y, position.z, 1);
        Vector4 ndcPos = new Vector4(clipPos.x / clipPos.w, clipPos.y / clipPos.w, clipPos.z / clipPos.w, 1);
        Vector4 screenPos = ViewPort * ndcPos;
        return screenPos;
    }



}
