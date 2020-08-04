using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public static TimerScript Instance {get; private set;}

    float time;



    void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
    }

    public float GetTime(){
        return time;
    }

    public void SetTime(float time){
        print("Time set to " + time);
        this.time = time;
    }

    


}
