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

    public Vector2 moveSpeed;

    public float maxMoveSpeed;
    private Vector3 startingScale;

    public Animator animator;

    void OnEnable(){
        rb = GetComponent<Rigidbody2D>();
        grounded = false;
        
        MoveRight = () => {StopCoroutine("Move"); StartCoroutine("Move", true);};
        MoveLeft = () => {StopCoroutine("Move"); StartCoroutine("Move", false);};

        StopRight = () => {if(rb.velocity.x > 0) StopCoroutine("Move");};
        StopLeft = () => {if(rb.velocity.x < 0) StopCoroutine("Move");};
        
        Jump = () => {if(grounded) rb.velocity += new Vector2(0, 3f);};
        SizeUp = () => StartCoroutine("Grow", 2f);
        startingScale = transform.localScale;
    }

    void Update() {
        UpdateGrounded();
        
        animator.SetFloat("LinearSpeed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", !grounded);
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
            if(grounded){
                if(!right) rb.velocity = -new Vector2(moveSpeed.x, rb.velocity.y);
                else rb.velocity = new Vector2(moveSpeed.x, rb.velocity.y);
            } else {
                if(!right) rb.velocity += -new Vector2(moveSpeed.x, 0) / 5f;
                else rb.velocity += new Vector2(moveSpeed.x, 0) / 5f;
            }

            if(rb.velocity.x > maxMoveSpeed){
                rb.velocity = new Vector2(maxMoveSpeed, rb.velocity.y);
            }
            if(rb.velocity.x < -maxMoveSpeed){
                rb.velocity = new Vector2(-maxMoveSpeed, rb.velocity.y);
            }

            if(transform.localScale.x > 0 && rb.velocity.x < -0.1f || 
               transform.localScale.x < 0 && rb.velocity.x > 0.1f){
                   transform.localScale = new Vector3(-1f * transform.localScale.x, transform.localScale.y, transform.localScale.z);
               }

            yield return null;
        }
    }

    public void Reset(){
        transform.localScale = startingScale;
        //...
    }

    void UpdateGrounded(){
        float dis = transform.GetComponent<CapsuleCollider2D>().bounds.size.y / 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, dis);

        if(hit.collider != null){
            grounded = false;

            Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + dis), Color.green);
        }else{
            grounded = true;
            Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + dis), Color.red);
        }

    }
}


