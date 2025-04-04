using UnityEngine;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System.Data.Common;
using System;

public class Lanbelder : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float speed = 16f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private Animator animator;

    [SerializeField] private BoxCollider2D boxCollider2D;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private bool canAttack;
    private int player;
    private float cooldownTimer = Mathf.Infinity;
    private Rigidbody2D rb;
    private bool isDashing = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody2D>();


        canAttack = playerCombat.canAttack;
        player = playerCombat.player;

    }

    void Update()
    {

        player = playerCombat.player;

        cooldownTimer += Time.deltaTime;

        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.I) && cooldownTimer > attackCooldown && canAttack)
            {
                Dash();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2) && cooldownTimer > attackCooldown && canAttack)
            {
                Dash();
            }
        }
    }

    void Dash()
    {
        // Reset cooldown timer so the ability can't be used again immediately.
        cooldownTimer = 0f;
        animator.SetBool("Lambo", true);
        boxCollider2D.size = new Vector2(3f, 0.5f);
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        playerMovement.enabled = false;
        Vector2 dashDirection = new Vector2();

        if (dashDirection == Vector2.zero)
        {
            dashDirection = playerMovement.isFacingRight ? Vector2.right : Vector2.left;
        }

        rb.linearVelocity += new Vector2(dashDirection.x * speed, rb.linearVelocity.y);

        StartCoroutine(EndDash(originalGravity));
    }


    private IEnumerator EndDash(float originalGravity)
    {
        yield return new WaitForSeconds(dashDuration);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerMovement.enabled = true;
        isDashing = false;
        animator.SetBool("Lambo", false);
        boxCollider2D.size = new Vector2(0.4f, 0.5f);
        rb.gravityScale = originalGravity;
    }
    // private IEnumerator Dash()
    // {
    //     isDashing = true;
    //     cooldownTimer = 0f;

    //     float originalGravity = rb.gravityScale;
    //     rb.gravityScale = 0;

    //     if (playerMovement.isFacingRight){
    //         rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
    //     }
    //     else{
    //         rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
    //     }


    //     //rb.linearVelocity = new Vector2(dashDirection.x , 0);

    //     yield return new WaitForSeconds(dashDuration);

    //     rb.linearVelocity = Vector2.zero;
    //     rb.gravityScale = originalGravity;
    //     isDashing = false;
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing && collision.CompareTag("Player"))
        {
            PlayerCombat enemy = collision.GetComponent<PlayerCombat>();

            if (enemy != null)
            {
                float knockbackForce = playerMovement.isFacingRight ? 75f : -75f;
                enemy.GetComponent<PlayerHealth>().TakeDamage(damage, knockbackForce);

                StopDash();
            }
        }
    }

    private void StopDash()
    {
        rb.linearVelocity = Vector2.zero;
        isDashing = false;
    }
}
