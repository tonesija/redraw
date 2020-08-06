using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MorphUIScript : MonoBehaviour
{
    GameObject morph1;
    GameObject morph2;
    GameObject morph3;

    public Image morph1Image;
    public Image morph2Image;
    public Image morph3Image;


    void UpdateUI(){
        if(morph1 != null) {
            morph1Image.color = Color.white;
            morph1Image.sprite = morph1.GetComponent<SpriteRenderer>().sprite;
        }
        if(morph2 != null) {
            morph2Image.color = Color.white;
            morph2Image.sprite = morph2.GetComponent<SpriteRenderer>().sprite;
        }
        if(morph3 != null) {
            morph3Image.color = Color.white;
            morph3Image.sprite = morph3.GetComponent<SpriteRenderer>().sprite;
        }

        
    }

    public bool HasMorphs(){
        return morph1 || morph2 || morph3;
    }

    public void SetMorph1(GameObject morph){
        morph1 = morph;
        UpdateUI();
    }

    public void SetMorph2(GameObject morph){
        morph2 = morph;
        UpdateUI();
    }

    public void SetMorph3(GameObject morph){
        morph3 = morph;
        UpdateUI();
    }

    public GameObject GetMorph1(){
        return morph1;
    }

    public GameObject GetMorph2(){
        return morph2;
    }

    public GameObject GetMorph3(){
        return morph3;
    }
}
