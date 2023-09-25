using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalarAndVectorMultiplication : MonoBehaviour
{
    [Range(-3, 3f)]
    public float Scalar;

    public Vector3 Destination = Vector3.one;

    private void OnDrawGizmos()
    {
        
        FGizmos.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.green;
        Vector3 pos = new Vector3(Destination.x * Scalar, Destination.y * Scalar, Destination.z * Scalar);
        //Vector3 pos = Scalar * Destination;
        FGizmos.DrawLineWithArrow(Vector3.zero, pos, 0.25f);
    }
}
