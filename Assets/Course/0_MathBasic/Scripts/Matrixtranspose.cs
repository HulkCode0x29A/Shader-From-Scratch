using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrixtranspose : MonoBehaviour
{
    
    void Start()
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;
        matrix[0, 3] = 4;
        Debug.Log(matrix);

        Matrix4x4 transposeMatrix = matrix.transpose;
        Debug.Log("--------------transpose--------------");
        Debug.Log(transposeMatrix);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
