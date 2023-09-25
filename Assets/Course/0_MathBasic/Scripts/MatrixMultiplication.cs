using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixMultiplication : MonoBehaviour
{
   
    void Start()
    {
        Matrix4x4 firstMatrix = Matrix4x4.identity;
        Debug.Log("---------------firstMatrix---------------");
        Debug.Log(firstMatrix);

        Matrix4x4 secondMatrix = Matrix4x4.identity;
        secondMatrix[0, 0] = 1;
        secondMatrix[0, 1] = 2;
        secondMatrix[0, 2] = 3;
        secondMatrix[0, 3] = 4;
        Debug.Log("---------------secondMatrix---------------");
        Debug.Log(secondMatrix);

        Matrix4x4 result = firstMatrix * secondMatrix;
        Debug.Log("---------------result---------------");
        Debug.Log(result);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
