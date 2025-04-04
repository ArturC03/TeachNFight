using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativargrito : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] public GameObject[] grito;

    private bool canAttack;
    private int player;
    private PlayerCombat playerCombat;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        playerCombat = GetComponent<PlayerCombat>();
        canAttack = playerCombat.canAttack;
        player = playerCombat.player;
    }
    private int Findgrito()
    {
        for (int i = 0; i < grito.Length; i++)
        {
            if (!grito[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    void Update()
    {
        player = playerCombat.player;
        cooldownTimer += Time.deltaTime;
        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.I) && cooldownTimer > attackCooldown && canAttack == true)
            {
                cooldownTimer += Time.deltaTime;

                AttackGrito();
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2) && canAttack)
            {
                cooldownTimer += Time.deltaTime;

                AttackGrito();
            }

        }

    }
    void AttackGrito()
    {
        cooldownTimer = 0;

        grito[Findgrito()].transform.position = this.transform.position;
        grito[Findgrito()].GetComponent<grito>().SetDirection(Mathf.Sign(transform.localScale.x));
        grito[Findgrito()].GetComponent<grito>().pai = this.gameObject;


        Debug.Log("Grito started");
    }



}








