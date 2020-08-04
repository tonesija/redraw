using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserScript : MonoBehaviour
{
    public static float duration = 1f;

    public float distance;

    Vector2 origPos;

    public Vector2 speed;

    void Awake()
    {   
        origPos = new Vector2(transform.position.x, transform.position.y);
        StartCoroutine("EraseAnimation");
    }

    IEnumerator EraseAnimation(){
        float time = 0;
        while(time <= duration){
            time += Time.deltaTime;

            if(Mathf.Abs((transform.position - (Vector3)origPos).magnitude) > distance) {
                speed = -speed;
                transform.position += (Vector3)speed * Time.deltaTime;
            }

            transform.position += (Vector3)speed * Time.deltaTime;

            yield return null;
        }
        Destroy(this.gameObject);
    }

}
