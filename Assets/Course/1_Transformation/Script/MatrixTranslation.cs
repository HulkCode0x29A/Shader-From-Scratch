using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTranslation : MonoBehaviour
{
    public Vector3 Translation;

    public Vector3 StartPos = Vector3.one;

    private void OnDrawGizmos()
    {
        FGizmos.DrawLHCoordinate(Vector3.zero);

        Gizmos.color = Color.red;
        Vector3 pos = StartPos;
        Gizmos.DrawSphere(pos, 0.25f);

        Matrix4x4 matrix = Matrix4x4.identity;
        matrix[0, 3] = Translation.x;
        matrix[1, 3] = Translation.y;
        matrix[2, 3] = Translation.z;

        //Expand to 4 elements, the fourth element is set to 1, otherwise the translation term will not work
        //Vector3 newPos = matrix * new Vector4(pos.x, pos.y, pos.z, 1);
        //or call a built-in function
        Vector3 newPos = matrix.MultiplyPoint(pos);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(newPos, 0.25f);
    }
}
