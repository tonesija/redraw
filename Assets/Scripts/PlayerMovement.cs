using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public MovementDelegate MoveRight;

    public MovementDelegate MoveLeft;

    public MovementDelegate Jump;

    public MovementDelegate SizeUp;



    public delegate void MovementDelegate();

    

    void OnEnable(){
        rb = GetComponent<Rigidbody2D>();
        

        MoveRight = () => rb.velocity = new Vector2(2f, rb.velocity.y);
        MoveLeft = () => rb.velocity = new Vector2(-2f, rb.velocity.y);
        Jump = () => rb.velocity += new Vector2(0, 3f);
        SizeUp = () => StartCoroutine("Grow", 2f);
   

    }

    IEnumerator Grow(float scalar){
        Vector3 limit = scalar * transform.localScale;
        while(transform.localScale.x <= limit.x){
            transform.localScale *= 1.1f;

            yield return new WaitForSeconds(0.1f);
        }
    }


    
    
}


