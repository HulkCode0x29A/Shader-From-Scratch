using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMatrixMultiplication : MonoBehaviour
{
    public Vector3 V1 = new Vector3(1,2,3);

    
    void Start()
    {
        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 0] = 2;
        matrix[1, 1] = 2;
        matrix[2, 2] = 2;
        matrix[3, 3] = 2;
        Debug.Log("--------------------original vector--------------------");
        Debug.Log(V1);

        Vector3 v = matrix * V1;
        Debug.Log("--------------------transformed vector--------------------");
        Debug.Log(v);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
