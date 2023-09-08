using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderFragmentBase 
{
    public int ID { get; set; }


    public virtual bool Fragment(Vector3 bcCoord, out Color outColor)
    {
        outColor = Color.black;
        return false;
    }
}
