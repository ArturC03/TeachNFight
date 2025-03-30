    using System.Collections;
using UnityEngine;


    public class camara : MonoBehaviour
    {
    [SerializeField] private float attackCooldown;
    [SerializeField] public GameObject[] grito;
    [SerializeField] public GameObject firepoint;

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
            if (Input.GetKeyDown(KeyCode.Keypad2) && cooldownTimer > attackCooldown && canAttack == true)
            {
                cooldownTimer += Time.deltaTime;

                AttackGrito();
            }

        }

    }
    void AttackGrito()
    {
        cooldownTimer = 0;

        grito[Findgrito()].transform.position = firepoint.transform.position;
        grito[Findgrito()].GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x));
        grito[Findgrito()].GetComponent<Bullet>().pai = this.gameObject;


        Debug.Log("Grito started");
    }


}





