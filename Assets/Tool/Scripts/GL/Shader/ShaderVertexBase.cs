using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderVertexBase
{
    public int ID{ get; set; }



    public virtual Vector4 Vertex() {
        return Vector4.zero;
    }


    
}
