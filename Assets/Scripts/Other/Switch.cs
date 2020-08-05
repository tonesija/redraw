using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public ISwitchable[] switchableObjects;

    public bool isRadio;

    bool toggle;

    void OnTriggerEnter2D(Collider2D other) {
        toggle = !toggle;


        foreach(ISwitchable switchable in switchableObjects){
            if(toggle || !isRadio){
                switchable.SwitchOn();
            }else{
                switchable.SwitchOff();
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(isRadio) return;
        foreach(ISwitchable switchable in switchableObjects){
            switchable.SwitchOff();
        }
    }
}
