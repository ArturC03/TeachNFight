using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb;
    private bool isFacingRight;
    private GameObject camare;
    private CircleCollider2D collidercamara;
    [SerializeField]
    private GameObject sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = GameObject.Find("Rubem").GetComponent<PlayerMovement>().isFacingRight;
        camare = GameObject.Find("Rubem").GetComponent<camara>().instanciacamara;

        // Definir a dire��o com base na face do jogador
        if (isFacingRight)
        {
            rb.linearVelocity = transform.right * speed;
        }
        else
        {
            rb.linearVelocity = -transform.right * speed;
        }



        // Destruir a bala ap�s 10 segundos
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        if (camare != null)
        {
            sprite.transform.Rotate(0, 0, 360 * Time.deltaTime); // Ajusta o valor 360 para controlar a velocidade de rotação.

            // Corrigir o uso do CircleCollider2D da bala
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);  // Ajuste o valor do raio conforme necess�rio
            foreach (Collider2D hit in hits)
            {
                Debug.Log($"Detected object: {hit.name}");
                Debug.Log($"Detected object: {hit.tag}");

                if (hit.CompareTag("Hurtbox"))
                {

                    PlayerCombat playerCombat = hit.transform.root.GetComponent<PlayerCombat>();
                    if (playerCombat != null && playerCombat.player != GameObject.Find("Rubem").GetComponent<camara>().player)
                    {
                        Debug.Log("Hit detected on enemy player");

                        PlayerHealth enemyHealth = hit.transform.root.GetComponent<PlayerHealth>();

                        if (enemyHealth != null)
                        {
                            // Aplicar dano baseado na posi��o
                            if (transform.position.x < hit.transform.position.x)
                            {
                                enemyHealth.TakeDamage(2, 10f);
                            }
                            else
                            {

                                enemyHealth.TakeDamage(2, -10f);
                            }
                        }
                        else
                        {
                            Debug.LogWarning("PlayerHealth component not found on the target");
                        }
                    }
                    else if (playerCombat == null)
                    {
                        Debug.LogWarning("PlayerCombat component not found on the target");
                    }
                }
            }
        }
    }
    }
