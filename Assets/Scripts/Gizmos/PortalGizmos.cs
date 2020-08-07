using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGizmos : MonoBehaviour
{
    private void OnDrawGizmos() {
        float angle = transform.eulerAngles.z;

        Gizmos.color = Color.blue;
        
        Gizmos.DrawLine(transform.position, transform.position + RotateVector(Vector3.left, angle - 180.0f));
        Gizmos.DrawLine(transform.position, transform.position + RotateVector(new Vector3(-0.1f,  0.1f, 0.0f), angle  - 135.0f));
        Gizmos.DrawLine(transform.position, transform.position + RotateVector(new Vector3(-0.1f, -0.1f, 0.0f), angle - 225.0f));

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + RotateVector(Vector3.right, angle));
        Gizmos.DrawLine(transform.position + RotateVector(Vector3.right, angle), transform.position + RotateVector(Vector3.right, angle) + RotateVector(new Vector3(-0.1f,  0.1f, 0.0f), angle - 135.0f));
        Gizmos.DrawLine(transform.position + RotateVector(Vector3.right, angle), transform.position + RotateVector(Vector3.right, angle) + RotateVector(new Vector3(-0.1f, -0.1f, 0.0f), angle + 135.0f));
    }

    public Vector3 RotateVector(Vector3 vector, float angleInDegrees){
        Vector3 rotatedVector = new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));

        return vector.magnitude * rotatedVector;
    }
}
