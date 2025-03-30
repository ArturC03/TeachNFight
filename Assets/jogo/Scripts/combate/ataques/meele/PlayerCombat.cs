using System.Collections;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{

    public BoxCollider2D basicPunch;
    public float cooldown = 0.5f;
    public bool canAttack = true;
	 [SerializeField] public int player;
    [SerializeField] public int damage;

    private Animator animator;
	private PlayerMovement movement;

    void Start()
    {
        basicPunch.enabled = false;
		animator = GetComponent<Animator>();
		movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
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
		animator.SetTrigger("Isattacking");
		canAttack = false;
       
        StartCoroutine(ResetAttackCooldown());
		basicPunch.enabled = true;
		Collider2D[] hits = Physics2D.OverlapBoxAll(basicPunch.bounds.center, basicPunch.bounds.size, 0f);
		foreach (Collider2D hit in hits)
		{
			
			if (hit.tag == "Hurtbox" && hit.transform.root != this.transform)
			{
				PlayerHealth enemyHealth = hit.transform.root.GetComponent<PlayerHealth>();
				if (movement.isFacingRight)
					enemyHealth.TakeDamage(damage, 100f);
				else
					enemyHealth.TakeDamage(damage, -100f);
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
		basicPunch.enabled = false;
	}
}