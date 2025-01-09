using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour
{

    public BoxCollider2D basicPunch;
    public float cooldown = 0.5f;
    public bool canAttack = true;
    public bool isAttacking = false;
	public int player;

    void Start()
    {
        basicPunch.enabled = false;
    }

    void Update()
    {
        //Debug.Log(isAttacking);
        if (Input.GetKeyDown(KeyCode.O) && canAttack && player == 1)
		{
				Attack();
		}
		else
		{
			//Disable animator
		}   
    }

    void Attack()
    {
        isAttacking = true;
		canAttack = false;
        Debug.Log("Attack");
        //Play attack animation
        StartCoroutine(ResetAttackCooldown());
		basicPunch.enabled = true;
		Collider2D[] hits = Physics2D.OverlapBoxAll(basicPunch.bounds.center, basicPunch.bounds.size, 0f);
		foreach (Collider2D hit in hits)
		{
			
			if (hit.tag == "Enemy")
			{
				PlayerHealth enemyHealth = hit.transform.root.GetComponent<PlayerHealth>();
				enemyHealth.TakeDamage(10, 0.1f);
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
	}
}