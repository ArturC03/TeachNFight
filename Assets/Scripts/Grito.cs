using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Grito : MonoBehaviour
{
    public bool isAttacking = false;
    public float cooldown = 5f;
    public bool canAttack = true;
    private int player;
    public GameObject grito;
    public CircleCollider2D colliderGrito;
    public GameObject instanciaGrito;
    //public GameObject animaçaogrito;
    public bool isGrito = false;

    void Start(){
        player = GetComponent<PlayerCombat>().player;
    }

    void Update()
    {
        //Debug.Log(isAttacking);
		if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.I) && canAttack)
            {
                AttackGrito();
                StartCoroutine(ResetAttackCooldown());
            }

            if (instanciaGrito != null){
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, colliderGrito.radius);
                foreach (Collider2D hit in hits)
                {
                    //Debug.Log($"Detected object: {hit.name}");
                    if (hit.CompareTag("Hurtbox"))
                    {
                        PlayerCombat playerCombat = hit.transform.root.GetComponent<PlayerCombat>();
                        if (playerCombat != null && playerCombat.player != this.player)
                        {
                            // Debug.Log("Hit detected on enemy player");

                            PlayerHealth enemyHealth = hit.transform.root.GetComponent<PlayerHealth>();

                            if (enemyHealth != null)
                            {
                                if (transform.position.x < hit.transform.position.x)
                                    enemyHealth.TakeDamage(1, 3f);
                                else
                                    enemyHealth.TakeDamage(1, -3f);
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
                    // Debug.Log("Detect");
                    // PlayerCombat playerCombat = hit.GetComponent<PlayerCombat>();
                    // if (hit.tag == "Player" && hit.GetComponent<PlayerCombat>().player != this.player)
                    // {
                    //     Debug.Log("Hit");
                    //     PlayerHealth enemyHealth = hit.transform.root.GetComponent<PlayerHealth>();
                    //     if (movement.isFacingRight)
                    //         enemyHealth.TakeDamage(20, 5f);
                    //     else
                    //         enemyHealth.TakeDamage(20, -5f);
                    // }
                }
            }
            
        }
    }

    IEnumerator ResetAttackCooldown()
	{
        canAttack = false;
		StartCoroutine(ResetAttackBool());
		yield return new WaitForSeconds(cooldown);
        cooldown = 5f;
		canAttack = true;
        
	}

	IEnumerator ResetAttackBool()
	{
		yield return new WaitForSeconds(0.2f);
        isAttacking = false;
        StopGrito();
	}

    void AttackGrito()
        {
            instanciaGrito = Instantiate(grito, transform.position, Quaternion.identity);
            colliderGrito = instanciaGrito.GetComponent<CircleCollider2D>();

        //if (!movement.isFacingRight)
        //{
        //    Flip();
        //}
        //else
        //{
        //}
            Debug.Log("Grito started");
            isGrito = true;        
    }

    void StopGrito()
        {
            Debug.Log("Grito stopped");
            Destroy(instanciaGrito);
            isGrito = false;
    }

    //private void Flip()

    //{

    //    Vector3 localScale = anima�aogrito.transform.localScale;

    //    localScale.x *= -1f;
    //    anima�aogrito.transform.position *= -1;
    //    anima�aogrito.transform.localScale = localScale;

    //}

}



 

