using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : ShaderBase
{
    [VertexLayout(0)]
    private Vector3 position;

    private Matrix4x4 Color;

    private Matrix4x4 ViewPort;

    public override Vector4 Vertex()
    {
        
        Matrix4x4 modelMatrix = FMatrix.Sclaing(new Vector3(2,2,2));
        Vector4 screenPos = ViewPort * modelMatrix * new Vector4(position.x, position.y, position.z, 1);
        return screenPos;
    }

    public override bool Fragment(Vector3 bcCoord, out Color outColor)
    {

        outColor = Color * new Vector4(bcCoord.x, bcCoord.y, bcCoord.z, 1);

        return false;
    }


}
