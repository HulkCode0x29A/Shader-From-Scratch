using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeVector : MonoBehaviour
{
    public GameObject Sphere;

    private void OnDrawGizmos()
    {
        GizmosExtension.DrawLHCoordinate(Vector3.zero);
        if(null != Sphere)
        {
            Gizmos.color = Color.green;
            
            GizmosExtension.DrawLineWithArrow(Vector3.zero, Sphere.transform.position,0.25f);

            //draw negativevector
            Gizmos.color = Color.magenta;
            GizmosExtension.DrawLineWithArrow(Vector3.zero, -Sphere.transform.position, 0.25f);

        }
    }
}
