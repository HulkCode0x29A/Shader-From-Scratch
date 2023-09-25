using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorAddition : MonoBehaviour
{
    public Vector3 V1;

    public Vector3 V2;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        FGizmos.DrawLineWithArrow(Vector3.zero, V1,0.25f);
        Gizmos.color = Color.red;
        FGizmos.DrawLineWithArrow(V1, V1+ V2,0.25f);
        Gizmos.color = Color.green;
        FGizmos.DrawLineWithArrow(Vector3.zero, V1 + V2,0.25f);
    }
}
