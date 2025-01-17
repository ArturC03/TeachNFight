using System.Collections;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{

    public BoxCollider2D basicPunch;
    public float cooldown = 0.5f;
    public bool canAttack = true;
    public bool isAttacking = false;
	public int player;
	public Animator animator;
	public PlayerMovement movement;

    void Start()
    {
        basicPunch.enabled = false;
		animator = GetComponent<Animator>();
		movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //Debug.Log(isAttacking);
		if (player == 1){
			if (Input.GetKeyDown(KeyCode.U) && canAttack)
			{
				Attack();
			}
		}
		else{
			if (Input.GetKeyDown(KeyCode.Keypad1) && canAttack)
			{
				Attack();
			}
		}
        
    }

    void Attack()
    {
		animator.SetBool("Isattacking", true);
        isAttacking = true;
		canAttack = false;
        //Debug.Log("Attack");
        //Play attack animation
        StartCoroutine(ResetAttackCooldown());
		basicPunch.enabled = true;
		Collider2D[] hits = Physics2D.OverlapBoxAll(basicPunch.bounds.center, basicPunch.bounds.size, 0f);
		foreach (Collider2D hit in hits)
		{
			
			if (hit.tag == "Hurtbox" && hit.transform.root.GetComponent<PlayerCombat>().player != this.player)
			{
				PlayerHealth enemyHealth = hit.transform.root.GetComponent<PlayerHealth>();
				if (movement.isFacingRight)
					enemyHealth.TakeDamage(5, 100f);
				else
					enemyHealth.TakeDamage(5, -100f);
			}
		}
    }

    IEnumerator ResetAttackCooldown()
	{
		StartCoroutine(ResetAttackBool());
		yield return new WaitForSeconds(cooldown);
        cooldown = 0.5f;
		canAttack = true;
	}

	IEnumerator ResetAttackBool()
	{
		yield return new WaitForSeconds(0.2f);
        isAttacking = false;
		basicPunch.enabled = false;
		animator.SetBool("Isattacking", false);
	}
}