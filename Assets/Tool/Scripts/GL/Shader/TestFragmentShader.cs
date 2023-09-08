using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFragmentShader : ShaderFragmentBase
{
    private Vector3 position;

    private Vector3 color;

    
    public override bool Fragment(Vector3 bcCoord, out Color outColor)
    {
        outColor = new Color(color.x, color.y, color.z, 1);
        return false;
    }
}
