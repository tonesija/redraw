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

    public TransformDelegate Morph;

    public delegate void TransformDelegate(GameObject toTransform);

    Rigidbody2D rb;

    bool grounded;

    public Vector2 moveSpeed;
    public float jumpForce;

    private Vector3 startingScale;

    public Animator animator;
    
    private bool right = true;
    private bool moving = false;



    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        grounded = false;
        
        MoveRight = () => {StopCoroutine("Move"); StartCoroutine("Move", true);};
        MoveLeft = () => {StopCoroutine("Move"); StartCoroutine("Move", false);};

        StopRight = () => {if(right) StopCoroutine("Move"); moving = false;};
        StopLeft = () => {if(!right) StopCoroutine("Move"); moving = false;};
        
        Jump = () => {if(grounded) rb.velocity += new Vector2(0, jumpForce);};
        SizeUp = () => StartCoroutine("Grow", 2f);

        Morph = (to) => {
            StartCoroutine("MorphTo", to);
        };

        startingScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Update() {
        UpdateGrounded();

        animator.SetFloat("LinearSpeed", moving ? 1.0f : 0.0f);
        animator.SetBool("IsJumping", !grounded);
    }

    IEnumerator Grow(float scalar){
        Vector3 limit = scalar * transform.localScale;
        while(transform.localScale.x <= limit.x){
            transform.localScale *= 1.1f;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator MorphTo(GameObject toMorph){
        Vector2 morphLoc = transform.position + (Vector3)new Vector2(0, toMorph.GetComponent<SpriteRenderer>().bounds.size.y / 2f);
        GameObject newObj = Instantiate(toMorph, morphLoc, Quaternion.identity);
        newObj.GetComponent<PlayerMorphs>().AddToResetableList();

        transform.position = new Vector2(-100, -100);

        yield return null;
        
    }

    IEnumerator Move(bool right){
        while(true){
            this.right = right;
            moving = true;

            if(Time.timeScale > 0.9f && 
              (transform.localScale.x > 0 && !right || 
               transform.localScale.x < 0 && right)){
                   transform.localScale = new Vector3(-1f * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            if(right){
                transform.Translate(moveSpeed * Time.deltaTime);
                
            }else{
                transform.Translate(-moveSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }

    public void Reset(){
        transform.localScale = startingScale;
        //...
    }

    void UpdateGrounded(){
        float dis = transform.GetComponent<CapsuleCollider2D>().bounds.size.y / 2f;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + dis), Vector3.down, dis * 1.1f);

        if(hit.collider != null){
            grounded = true;

        }else{
            grounded = false;
        }

    }
}


