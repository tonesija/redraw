using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : ISwitchable
{
    public Vector2 location;

    public float speed;

    Vector2 dir;

    Vector2 startLocation;

    bool moving;

    bool toLocation;

    float distance;
    float moved;

    void Start()
    {
        toLocation = true;
        dir = (-transform.position + (Vector3)location).normalized;
        startLocation = transform.position;
        distance = (transform.position - (Vector3)location).magnitude;
    }

    void Update()
    {
        if(moving){
            if(toLocation){
                transform.Translate(speed * dir * Time.deltaTime);
            }else {
                transform.Translate(-speed * dir * Time.deltaTime);
            }
            moved += speed * Time.deltaTime;

            if(moved >= distance){
                toLocation = !toLocation;
                moved = 0f;
            }
        }
    }

    override public void SwitchOn(){
        moving = true;
    }

    override public void SwitchOff(){
        moving = false;
    }
}
