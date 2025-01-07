using Unity.Mathematics;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform tr;
    public Rigidbody2D rb;
    public int health = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage, float knockbackMultiplier){
        Vector2 knockback = new Vector2(knockbackMultiplier * 0.5f, knockbackMultiplier * 0.5f);
        if (health > 0){
            knockback = new Vector2(knockbackMultiplier * math.sqrt(health), knockbackMultiplier * math.sqrt(health));
        }
        health += damage;
        rb.linearVelocity = knockback;
        Debug.Log(health);
    }
}
