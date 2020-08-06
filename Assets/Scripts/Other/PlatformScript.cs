using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : ISwitchable
{

    public bool isGate;

    public bool isSpringPlatform;
    
    public Vector2 endingLocation;
    

    public float movingSpeed;

    private Vector2 startingLocation;

    private Vector2 dir;
    private bool moving;

    private bool toOrFrom;
    private float distanceTraveled;

    private float distanceToTravel;
    private bool stopSpring;

    void Start()
    {
        toOrFrom = !isSpringPlatform;
        moving = false;
        stopSpring = true;

        startingLocation = transform.position;

        if(isGate){
            endingLocation = startingLocation + new Vector2(0.0f, 2.0f);
            isSpringPlatform = true;
        }

        dir = (-startingLocation + endingLocation).normalized;
        
        distanceTraveled = 0.0f;
        distanceToTravel = Vector2.Distance(startingLocation, endingLocation);

        
        
    }

    void Update()
    {

        if(isSpringPlatform){
            if(toOrFrom != moving){
                toOrFrom = moving;
                distanceToTravel = Vector2.Distance(transform.position, toOrFrom ? endingLocation : startingLocation);
                stopSpring = false;
                distanceTraveled = 0.0f;
            }

            if(!stopSpring){
                MovePlatform();
            }
            
        }else{
            if(moving){
                MovePlatform();
            }
        }
    }

    void MovePlatform(){

        if(distanceTraveled < distanceToTravel){
            float multiplier = toOrFrom ? 1.0f : -1.0f;
            transform.Translate(multiplier * movingSpeed * dir * Time.deltaTime);
            distanceTraveled += movingSpeed * Time.deltaTime;
        }else {
            if(!isSpringPlatform){
                toOrFrom ^= true;
            }else{
                stopSpring = true;
            }

            distanceTraveled = 0.0f;
            
        }

    }

    override public void SwitchOn(){
        moving = true;
    }

    override public void SwitchOff(){
        moving = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.activeSelf){
            other.transform.parent = null;
        }
    }
}
