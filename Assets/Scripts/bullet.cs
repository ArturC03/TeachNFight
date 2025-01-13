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
    private bool isFacingRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = GameObject.Find("Rubem").GetComponent<PlayerMovement>().isFacingRight;

        // Atualizar isfacingright com a direção atual do jogador
        if (isFacingRight)
        {

            rb.linearVelocity = transform.right * speed;

        }
        else
        {
            rb.linearVelocity = -transform.right * speed;

        }

        // Destroy the bullet after a certain lifetime
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
      
      
    }
}
