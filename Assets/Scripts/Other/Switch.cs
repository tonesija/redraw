using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public ISwitchable[] switchableObjects;

    public Sprite SwitchUp;
    public Sprite SwitchDown;

    public bool isRadio;

    bool toggle;

    int counter;

    void OnTriggerEnter2D(Collider2D other) {
        toggle = !toggle;
        counter++;

        if(toggle || !isRadio){
            GetComponent<SpriteRenderer>().sprite = SwitchDown;
        }else{
            GetComponent<SpriteRenderer>().sprite = SwitchUp;
        }


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

        counter--;

        foreach(ISwitchable switchable in switchableObjects){
            if(counter == 0){
                GetComponent<SpriteRenderer>().sprite = SwitchUp;
                switchable.SwitchOff();
            }
        }
    }
}
