using UnityEngine;

public class bufflino : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float buffDuration = 5f;
    [SerializeField] private float buffScaleMultiplier = 1.5f;
    [SerializeField] private float speed;
    private PlayerMovement playerMovement;
    private int damage;
    private bool canAttack;
    private int player;
    private PlayerCombat playerCombat;
    private float cooldownTimer = Mathf.Infinity;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Color originalColor;

    private void Awake()
    {
        
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        canAttack = playerCombat.canAttack;
        player = playerCombat.player;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (player == 1)
        {
            if (Input.GetKeyDown(KeyCode.I) && cooldownTimer > attackCooldown && canAttack)
            {
                ativarBuff();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad2) && cooldownTimer > attackCooldown && canAttack)
            {
                ativarBuff();
            }
        }
    }

    private void ativarBuff()
    {
        originalScale = transform.localScale;

        speed = playerMovement.speed;
        damage = playerCombat.damage;
        cooldownTimer = 0f;
        playerMovement.speed = speed / 2;
        playerCombat.damage = 15;
        spriteRenderer.color = Color.red;
        
        transform.localScale = new Vector3(originalScale.x * buffScaleMultiplier, originalScale.y * buffScaleMultiplier, originalScale.z);
        
       
        Invoke("resetBuff", buffDuration);
    }

    private void resetBuff()
    {
        originalScale = transform.localScale;

        transform.localScale = new Vector3(originalScale.x / buffScaleMultiplier, originalScale.y / buffScaleMultiplier, originalScale.z); ;

        spriteRenderer.color = originalColor;
        playerMovement.speed = speed;
        playerCombat.damage = damage;
    }
}
