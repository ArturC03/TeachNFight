using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int player;
    public Transform tr;
    public Rigidbody2D rb;
    public int health = 0;
    public bool isKnockback = false;
    public void Start(){
        player = GetComponent<PlayerCombat>().player;
    }
    public void Update()
    {
        if (isKnockback && rb.linearVelocity.magnitude == 0)
        {
            isKnockback = false;
        }
    }

    public void TakeDamage(int damage, float knockbackMultiplier)
    {
        Vector2 knockback;
        isKnockback = true;

        if (health > 0)
        {
            // Scale knockback with health and emphasize horizontal movement
            knockback = new Vector2(
                knockbackMultiplier * math.sqrt(health),  // Horizontal component
                math.abs(knockbackMultiplier) * math.sqrt(health) * 0.75f // Slightly reduce vertical component for balance
            );
        }
        else
        {
            // Apply a small default knockback for zero health
            knockback = new Vector2(
                knockbackMultiplier * 0.5f,  // Horizontal component
                math.abs(knockbackMultiplier) * 0.5f // Vertical component
            );
        }

        // Update health
        health += damage;

        // Apply knockback directly to velocity
        rb.linearVelocity += knockback * Time.deltaTime;

        // Debugging
        Debug.Log($"Knockback: {knockback}, Linear Velocity: {rb.linearVelocity}");
    }
}
