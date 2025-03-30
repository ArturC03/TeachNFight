using Unity.Mathematics;
using UnityEditor.ShaderGraph;
using UnityEngine;
 
public class PlayerMovement : MonoBehaviour

{
    private int player;  
    //movimento
    private PlayerHealth playerHealth;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 10f;
    public bool isFacingRight = true;
    //pegar componentes
    public Animator animator;    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    
 
    void Start(){
        player = GetComponent<PlayerCombat>().player;
        playerHealth = GetComponent<PlayerHealth>();
    }
    
    void Update()
    {
        if (player == 1){
            
            if (Input.GetKey(KeyCode.D)){
                horizontal = 1f;
            }
            else if (Input.GetKey(KeyCode.A)){
                horizontal = -1f;
            }
            else{
                horizontal = 0f;
            }
            animator.SetFloat("speed",math.abs(horizontal));
            if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            }
    
            if (Input.GetKeyUp(KeyCode.W) && rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
        }
        else{
            if (Input.GetKey(KeyCode.RightArrow)){
                horizontal = 1f;
            }
            else if (Input.GetKey(KeyCode.LeftArrow)){
                horizontal = -1f;
            }
            else{
                horizontal = 0f;
            }
            animator.SetFloat("speed",math.abs(horizontal));
            if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
             
            }
    
            if (Input.GetKeyUp(KeyCode.UpArrow) && rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
        }
            
            Flip();
    }
    
 
    public void onlanding()
    {
        // animator.SetBool("isjumping", false);
    }
 
    private void FixedUpdate()
    {
        if (!playerHealth.isKnockback){
            if (horizontal != 0){
                rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
            }
            else{
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y );
            }        
        }
        else{
            if (rb.linearVelocity.magnitude >= 0 && IsGrounded()){
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                playerHealth.isKnockback = false;
            }
        }
        
    }

    public bool IsGrounded()
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
