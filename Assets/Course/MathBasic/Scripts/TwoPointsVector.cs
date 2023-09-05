using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointsVector : MonoBehaviour
{


    public GameObject Sphere1;

    public GameObject Sphere2;


    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        if (null == Sphere1 || null == Sphere2)
            return;

        Gizmos.color = Color.green;
        Vector3 p1 = Sphere1.transform.position;
        Vector3 p2 = Sphere2.transform.position;
        Vector3 r = p1 - p2 ;
        FGizmos.DrawLineWithArrow(p2, p2+ r, 0.25f);
    }
}
