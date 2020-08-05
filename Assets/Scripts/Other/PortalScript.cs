using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private Vector3 portalExitLocation;
    private void Start() {
        portalExitLocation = transform.GetChild(0).position;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        other.transform.position = portalExitLocation;
    }
}
