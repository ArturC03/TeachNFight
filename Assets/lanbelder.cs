using UnityEngine;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Lanbelder : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float dashDuration = 5f;

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

        cooldownTimer += Time.deltaTime;

        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.I) && cooldownTimer > attackCooldown && canAttack)
            {
                StartCoroutine(Dash());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2) && cooldownTimer > attackCooldown && canAttack)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        cooldownTimer = 0f;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        rb.linearVelocity = new Vector2(playerMovement.horizontal, 0).normalized;

        //rb.linearVelocity = new Vector2(dashDirection.x , 0);

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDashing && collision.CompareTag("Player"))
        {
            PlayerCombat enemy = collision.GetComponent<PlayerCombat>();

            if (enemy != null)
            {
                float knockbackForce = playerMovement.isFacingRight ? 150f : -150f;
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
