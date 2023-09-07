using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderBase
{
    public int ID{ get; set; }



    public virtual Vector4 Vertex() {
        return Vector4.zero;
    }

    public virtual bool Fragment(Vector3 bcCoord, out Color outColor)
    {
        outColor = Color.black;
        return false;
    }
    
}
