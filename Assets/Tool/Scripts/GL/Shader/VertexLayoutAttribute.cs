using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexLayoutAttribute : Attribute
{
    private int index;
    public VertexLayoutAttribute(int index)
    {
        this.index = index;
    }

    public int Index
    {
        get { return index; }
        set { this.index = value; }
    }
}
