using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizedVector : MonoBehaviour
{
    public GameObject Sphere;

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        if (null == Sphere)
            return;

        Gizmos.color = Color.green;
        Vector3 pos = Sphere.transform.position;
        FGizmos.DrawLineWithArrow(Vector3.zero, pos, 0.25f);

        //normalize vector
        if(pos != Vector3.zero)
        {
            Gizmos.color = Color.yellow;
            Vector3 normalizedVector = pos / pos.magnitude;
            //Vector3 normalizedVector = pos.normalized;
            FGizmos.DrawLineWithArrow(Vector3.zero, normalizedVector, 0.25f);
        }

    }
}
