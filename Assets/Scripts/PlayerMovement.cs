using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float jumpPower;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] public Rigidbody2D rb;

    [Header("Transform")]
    private float horizontal;
    private float speed = 8f;
    private Vector2 directionalInput;

    [Header("Boolean")]
    private bool isFacingRight = true;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && isGrounded())
        {
            PlayJumpAnimation();
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            PlayJumpAnimation();
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        PlayerManager.Instance.animPlayer.SetBool("isGrounded", isGrounded());
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        SetDirectionalInput(directionalInput);
    }

    private void FixedUpdate()
    {

        //  PlayerManager.Instance.animPlayer.SetFloat("speed",Mathf.Abs(horizontal) );

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    void PlayJumpAnimation()
    {
        PlayerManager.Instance.animPlayer.SetTrigger("jump");
    }
    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
        PlayerManager.Instance.animPlayer.SetFloat("speed", Mathf.Abs(horizontal));

        if (directionalInput.x < 0)
        {
            //hadap kiri
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (directionalInput.x > 0)
        {
            //hadap kanan
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        // print(directionalInput);

    }
    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}