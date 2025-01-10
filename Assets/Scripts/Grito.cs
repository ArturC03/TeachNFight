using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Grito : MonoBehaviour
{
    public bool canAttack = true;
    public int player;
    public GameObject grito;
    public CircleCollider2D colliderGrito;
    public GameObject instanciaGrito;
    public bool isGrito = false;
    public PlayerMovement movement;

    void Start(){
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //Debug.Log(isAttacking);
		if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.I) && canAttack)
            {
                AttackGrito();
            }
            else if (Input.GetKeyUp(KeyCode.I) && instanciaGrito != null)
            {
                StopGrito();
            }

            if (instanciaGrito != null){
                Collider2D[] hits = Physics2D.OverlapCircleAll(grito.transform.position, colliderGrito.radius);
                foreach (Collider2D hit in hits)
                {
                    Debug.Log("Detect");
                    if (hit.tag == "Player" && hit.GetComponent<PlayerCombat>().player != this.player)
                    {
                        Debug.Log("Hit");
                        PlayerHealth enemyHealth = hit.transform.root.GetComponent<PlayerHealth>();
                        if (movement.isFacingRight)
                            enemyHealth.TakeDamage(20, 5f);
                        else
                            enemyHealth.TakeDamage(10, -5f);
                    }
                }
            }
            
        }
    }
    void AttackGrito()
        {
            instanciaGrito = Instantiate(grito, transform.position, Quaternion.identity);
            colliderGrito = instanciaGrito.GetComponent<CircleCollider2D>();
            Debug.Log("Grito started");
            isGrito = true;
        }

    void StopGrito()
        {
            Debug.Log("Grito stopped");
            Destroy(instanciaGrito);
            isGrito = false;
        }
    }
