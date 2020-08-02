using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementDelegate MoveRight;
    public MovementDelegate MoveLeft;
    public MovementDelegate Jump;
    public MovementDelegate SizeUp;
    public MovementDelegate StopRight;
    public MovementDelegate StopLeft;
    public delegate void MovementDelegate();

    Rigidbody2D rb;

    bool grounded;

    void OnEnable(){
        rb = GetComponent<Rigidbody2D>();
        grounded = false;
        
        MoveRight = () => {StopCoroutine("Move"); StartCoroutine("Move", true);};
        MoveLeft = () => {StopCoroutine("Move"); StartCoroutine("Move", false);};

        StopRight = () => {if(rb.velocity.x > 0) StopCoroutine("Move");};
        StopLeft = () => {if(rb.velocity.x < 0) StopCoroutine("Move");};
        
        Jump = () => {if(grounded) rb.velocity += new Vector2(0, 3f);};
        SizeUp = () => StartCoroutine("Grow", 2f);
    }

    void Update() {
        UpdateGrounded();
    }

    IEnumerator Grow(float scalar){
        Vector3 limit = scalar * transform.localScale;
        while(transform.localScale.x <= limit.x){
            transform.localScale *= 1.1f;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Move(bool right){
        while(true){
            float speed = 2f;
            if(!right) speed = -speed;

            rb.velocity = new Vector2(speed, rb.velocity.y);

            yield return null;
        }
    }

    public void Reset(){
        transform.localScale = new Vector3(1f, 1f, 1f);
        //...
    }

    void UpdateGrounded(){
        float dis = transform.GetComponent<SpriteRenderer>().bounds.size.y / 1.6f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, dis);

        if(hit.collider != null){
            grounded = true;
        }else{
            grounded = false;
        }
    }
}


