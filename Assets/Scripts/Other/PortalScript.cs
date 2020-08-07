using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Transform portalExit;

    private Vector3 portalExitLocation;
    private float portalExitAngle;

    private void Start() {
        portalExitLocation = portalExit.position;
        portalExitAngle = portalExit.eulerAngles.z;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        Rigidbody2D playerRB = other.gameObject.GetComponent<Rigidbody2D>();
        Vector2 velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y);
        
        if(Vector2.Angle(velocity, transform.position - other.transform.position) < 90.0f){

            
            
            float velocityAngle = Vector2.Angle(Vector2.right, velocity);
            float velocityMag = velocity.magnitude;
            

            if(velocity.y < 0.0f) velocityAngle *= -1.0f;

            float portalEntranceAngle = transform.eulerAngles.z;

            float velocityPortalRelativeAngle = velocityAngle - portalEntranceAngle;

            float exitVelocityAngle = portalExitAngle + velocityPortalRelativeAngle;

            Vector2 newVelocity = velocityMag * new Vector2(Mathf.Cos(exitVelocityAngle * Mathf.Deg2Rad), Mathf.Sin(exitVelocityAngle * Mathf.Deg2Rad));
            
            print("Player velocity angle: " + velocityAngle + "\nPortal entrance angle: " + portalEntranceAngle + "\nPortal exit angle: " + portalExitAngle);
            
            print("New velocity:" + newVelocity);
            other.transform.position = portalExitLocation;

            playerRB.velocity = newVelocity;
        }
    }

}
