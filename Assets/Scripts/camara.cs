using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class camara : MonoBehaviour
{
    public bool isAttacking = false;
    public float cooldown = 5f;
    public bool canAttack = true;
    private int player;
    private bool isfacingright;
    public GameObject camare;
    public CircleCollider2D collidercamara;
    public GameObject instanciacamara;
    public bool iscamara = false;

    void Start()
    {
        player = GetComponent<PlayerCombat>().player;
        isfacingright= GetComponent<PlayerMovement>().isFacingRight;
    }

    void Update()
    {
        Debug.Log(isAttacking);
        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.P) && canAttack)
            {
                Attackcamara();
                StartCoroutine(ResetAttackCooldown());
            }

            if (instanciacamara != null)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, collidercamara.radius);
                foreach (Collider2D hit in hits)
                {
                    Debug.Log($"Detected object: {hit.name}");
                    if (hit.CompareTag("Hurtbox"))
                    {
                        PlayerCombat playerCombat = hit.transform.root.GetComponent<PlayerCombat>();
                        if (playerCombat != null && playerCombat.player != this.player)
                        {
                             Debug.Log("Hit detected on enemy player");

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
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2) && canAttack)
            {
                Attackcamara();
                StartCoroutine(ResetAttackCooldown());
            }

            if (instanciacamara != null)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, collidercamara.radius);
                foreach (Collider2D hit in hits)
                {
                    Debug.Log($"Detected object: {hit.name}");
                    if (hit.CompareTag("Hurtbox"))
                    {
                        PlayerCombat playerCombat = hit.transform.root.GetComponent<PlayerCombat>();
                        if (playerCombat != null && playerCombat.player != this.player)
                        {
                             Debug.Log("Hit detected on enemy player");

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
                }
            }
        }

        IEnumerator ResetAttackCooldown()
        {
            canAttack = false;
            //StartCoroutine(ResetAttackBool());
            yield return new WaitForSeconds(cooldown);
            cooldown = 5f;
            canAttack = true;

        }

        //IEnumerator ResetAttackBool()
        //{
        //    yield return new WaitForSeconds(2f);
        //    isAttacking = false;
        //    stopcamara();
        //}

        void Attackcamara()
        {
            instanciacamara = Instantiate(camare, transform.position, Quaternion.identity);
            Bullet bulletScript = instanciacamara.GetComponent<Bullet>();
            bulletScript.isfacingright = isfacingright;
            collidercamara = instanciacamara.GetComponent<CircleCollider2D>();

            //if (!movement.isFacingRight)
            //{
            //    Flip();
            //}
            //else
            //{
            //}
            Debug.Log("camara started");
            iscamara = true;
        }

        //void stopcamara()
        //{
        //    Debug.Log("camara stopped");
        //    Destroy(instanciacamara);
        //    iscamara = false;
        //}

        

    }
}





