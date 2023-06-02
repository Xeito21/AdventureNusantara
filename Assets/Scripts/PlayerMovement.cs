using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);            
        }
        Flip();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * speed , rb.velocity.y);
    }

    private bool isGrounded () {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }
    private void Flip () {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
