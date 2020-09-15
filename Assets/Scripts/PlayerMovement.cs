using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public int extraJumpsValue;
    public float checkRadius;
    public float jumpForce;
    public Transform groundCheck;

    public LayerMask whatIsGround;

    private int extraJumps;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Vector2 movement;
    private new Transform transform;

    void Start() {
        extraJumps = extraJumpsValue;
        rb = this.GetComponent<Rigidbody2D>();
        transform = this.GetComponent<Transform>();
    }

    void FixedUpdate() {
        getInput();
        move();
    }

    private void getInput(){
        movement.x = Input.GetAxisRaw("Horizontal"); 
    }

    private void move(){
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
    }

    private void jump(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if(isGrounded){
            extraJumps = extraJumpsValue;
        }

        if(movement.y > 0 && extraJumps > 0){
            extraJumps--;
            rb.velocity = Vector2.up * jumpForce;
        }else if(movement.y > 0 && extraJumps == 0 && isGrounded){
            rb.velocity = Vector2.up * jumpForce;
        }
    }


}
