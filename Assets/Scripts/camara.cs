    using System.Collections;
    using UnityEngine;


    public class camara : MonoBehaviour
    {
        public bool isAttacking = false;
        public float cooldown = 5f;
        public bool canAttack = true;
        public int player;
        public GameObject camare;
        public CircleCollider2D collidercamara;
        public GameObject instanciacamara;
        public bool iscamara = false;

        void Start()
        {
            player = GetComponent<PlayerCombat>().player;
        }

        void Update()
        {
            if (player == 1)
            {
                if (Input.GetKeyDown(KeyCode.P) && canAttack)
                {
                    Attackcamara();
                    StartCoroutine(ResetAttackCooldown());
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
            else
            {
                if (Input.GetKeyDown(KeyCode.Keypad2) && canAttack)
                {
                    Attackcamara();
                    StartCoroutine(ResetAttackCooldown());
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





