using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public ISwitchable[] switchableObjects;

    public Sprite SwitchUp;
    public Sprite SwitchDown;

    public bool isRadio;
    public bool canBeSwitchedOff = true;

    bool toggle;

    int counter;

    void OnTriggerEnter2D(Collider2D other) {
        toggle = !toggle;
        counter++;

        if(toggle || !isRadio){
            GetComponent<SpriteRenderer>().sprite = SwitchDown;
        }else{
            if(canBeSwitchedOff || !isRadio){
                GetComponent<SpriteRenderer>().sprite = SwitchUp;
            }
        }


        foreach(ISwitchable switchable in switchableObjects){
            if(toggle || !isRadio){
                switchable.SwitchOn();
            }else{
                if(canBeSwitchedOff || !isRadio){
                    switchable.SwitchOff();
                }
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(isRadio) return;

        counter--;

        foreach(ISwitchable switchable in switchableObjects){
            if(counter == 0){
                GetComponent<SpriteRenderer>().sprite = SwitchUp;
                switchable.SwitchOff();
            }
        }
    }
}
