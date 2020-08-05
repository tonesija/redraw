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
        Rigidbody2D playerRB = other.gameObject.GetComponent<Rigidbody2D>();
        Vector2 velocity = playerRB.velocity;
        float angle = (transform.eulerAngles.z + 180) * Mathf.Deg2Rad;
        Vector2 normal = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        Vector2 newVelocity = Vector2.Reflect(velocity, normal);

        float angleBetweenPortals = angle + transform.GetChild(0).localEulerAngles.z * Mathf.Deg2Rad;
        print("Kut izmedu portala: " + angleBetweenPortals);

        float mag = velocity.magnitude;

        newVelocity = new Vector2(mag * Mathf.Cos(angleBetweenPortals), mag * Mathf.Sin(angleBetweenPortals));
        other.transform.position = portalExitLocation;

        playerRB.velocity = newVelocity;
    }
}
