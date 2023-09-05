using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeVector : MonoBehaviour
{
    public GameObject Sphere;

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);
        if(null != Sphere)
        {
            Gizmos.color = Color.green;
            
            FGizmos.DrawLineWithArrow(Vector3.zero, Sphere.transform.position,0.25f);

            //draw negativevector
            Gizmos.color = Color.magenta;
            FGizmos.DrawLineWithArrow(Vector3.zero, -Sphere.transform.position, 0.25f);

        }
    }
}
