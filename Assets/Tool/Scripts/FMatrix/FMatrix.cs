using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FMatrix 
{
    public static Matrix4x4 Translation(Vector3 trans)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = trans.x;
        matrix[1, 3] = trans.y;
        matrix[2, 3] = trans.z;
        return matrix;
    }

    public static Matrix4x4 RoateX(float theta)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[1, 1] = Mathf.Cos(theta * Mathf.Deg2Rad);
        matrix[1, 2] = -Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[2, 1] = Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[2, 2] = Mathf.Cos(theta * Mathf.Deg2Rad);

        return matrix;
    }

    public static Matrix4x4 RoateY( float theta)
    {
       
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0,0] = Mathf.Cos(theta * Mathf.Deg2Rad);
        matrix[0,2] = Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[2,0] = -Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[2,2] = Mathf.Cos(theta * Mathf.Deg2Rad);

        return matrix;
    }

    public static Matrix4x4 RoateZ(float theta)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta * Mathf.Deg2Rad);
        matrix[0, 1] = -Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[1,0] = Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[1,1]= Mathf.Cos(theta * Mathf.Deg2Rad);

        return matrix;

    }

    public static Matrix4x4 Sclaing(Vector3 scale)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = scale.x;
        matrix[1, 1] = scale.y;
        matrix[2, 2] = scale.z;

        return matrix;
    }

    public static Matrix4x4 Lookat(Vector3 from ,Vector3 to, Vector3 up)
    {
        Vector3 zAxis = (from - to).normalized;
        Vector3 yAxis = up.normalized;
        Vector3 xAxis = Vector3.Cross(yAxis, zAxis);
        yAxis = Vector3.Cross(zAxis, xAxis);

        //set xAxis to first row
        

        return Matrix4x4.identity;
    }
}
