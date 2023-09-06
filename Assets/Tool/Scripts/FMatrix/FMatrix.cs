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

    public static Matrix4x4 RoateY(float theta)
    {

        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta * Mathf.Deg2Rad);
        matrix[0, 2] = Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[2, 0] = -Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[2, 2] = Mathf.Cos(theta * Mathf.Deg2Rad);

        return matrix;
    }

    public static Matrix4x4 RoateZ(float theta)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = Mathf.Cos(theta * Mathf.Deg2Rad);
        matrix[0, 1] = -Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[1, 0] = Mathf.Sin(theta * Mathf.Deg2Rad);
        matrix[1, 1] = Mathf.Cos(theta * Mathf.Deg2Rad);

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

    public static Matrix4x4 Lookat(Vector3 from, Vector3 to, Vector3 up)
    {
        Vector3 zAxis = (from - to).normalized;
        Vector3 yAxis = up.normalized;
        Vector3 xAxis = Vector3.Cross(yAxis, zAxis);
        yAxis = Vector3.Cross(zAxis, xAxis);

        //xAxis.x   xAxis.y   xAxis.z    0
        //yAxis.x   yAxis.y   yAxis.z    0
        //zAxis.x   zAxis.y   zAxis.z    0
        //  0           0           0           1
        Matrix4x4 matrix = Matrix4x4.identity;
        //set xAxis to first row
        matrix[0, 0] = xAxis.x;
        matrix[0, 1] = xAxis.y;
        matrix[0, 2] = xAxis.z;
        //set yAxis to second row
        matrix[1, 0] = yAxis.x;
        matrix[1, 1] = yAxis.y;
        matrix[1, 2] = yAxis.z;
        //set zAxis to third row
        matrix[2, 0] = zAxis.x;
        matrix[2, 1] = zAxis.y;
        matrix[2, 2] = zAxis.z;


        return matrix;
    }

    /// <summary>
    /// get perspective matrix
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="bottom"></param>
    /// <param name="top"></param>
    /// <param name="zNear"></param>
    /// <param name="zFar"></param>
    /// <returns></returns>
    public static Matrix4x4 Perspective(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = -2 * zNear / (right - left);
        matrix[0, 2] = (right + left) / (right - left);
        matrix[1, 1] = -2 * zNear / (top - bottom);
        matrix[1, 2] = (top + bottom) / (top - bottom);
        matrix[2, 2] = (zNear + zFar) / (zNear - zFar);
        matrix[2, 3] = -2 * zNear * zFar / (zNear - zFar);
        matrix[3, 2] = -1;
        matrix[3, 3] = 0;
        return matrix;
    }
    
    /// <summary>
    /// get viewport matrix
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="bottom"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static Matrix4x4 ViewPort(int left, int right, int bottom, int top)
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = (float)(right - left) / 2;
        matrix[0, 3] = (float)(right + left) / 2;
        matrix[1, 1] = (float)(top - bottom) / 2;
        matrix[1, 3] = (float)(top + bottom) / 2;
        matrix[2, 2] = 1.0f / 2.0f;
        matrix[2, 3] = 1.0f / 2.0f;
        return matrix;
    }
}
