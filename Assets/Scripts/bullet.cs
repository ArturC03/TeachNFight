using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    private Rigidbody2D rb;
    private CircleCollider2D colllider;
    public PlayerMovement mov;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mov = GetComponent<PlayerMovement>();
    
        if(mov.isFacingRight)
        {
            rb.linearVelocity = transform.right * speed;

        }
        else
        {
            rb.linearVelocity = transform.right * speed;

        }
        rb.linearVelocity = transform.right * speed;

        // Destroy the bullet after a certain lifetime
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
      
      
    }
}
