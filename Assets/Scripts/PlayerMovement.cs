using System.Collections;

using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;
 
public class PlayerMovement : MonoBehaviour

{

    //animator

    private bool wasJumping;

    private float groundCheckDelay = 0.1f; 

    private float lastTimeGrounded;
 
    //movimento

    private float horizontal;

    private float speed = 8f;

    private float jumpingPower = 10f;

    public bool isFacingRight = true;
 
    //pegar componentes

    //public Animator animator;    

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask groundLayer;
 
    void Update()

    {

        horizontal = Input.GetAxisRaw("Horizontal");

        //animator.SetFloat("speed",math.abs(horizontal));

        if (Input.GetButtonDown("Jump") && IsGrounded())

        {

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

            //animator.SetBool("isjumping", true);

            wasJumping = true;

        }

 
 
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)

        {

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

 
        }
 
        if (Time.time - lastTimeGrounded > groundCheckDelay && IsGrounded() && wasJumping)

        {

            onlanding(); 

            wasJumping = false;

        }
 
        if (IsGrounded())

        {

            lastTimeGrounded = Time.time;

        }
 
        Flip();

    }
 
    public void onlanding()

    {

        //animator.SetBool("isjumping", false);

    }
 
    private void FixedUpdate()

    {

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

    }
 
    private bool IsGrounded()

    {

        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

    }
 
    private void Flip()

    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)

        {

            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;

            localScale.x *= -1f;

            transform.localScale = localScale;

        }

    }

}

 