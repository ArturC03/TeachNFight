using UnityEngine;
using UnityEngine.UI;

public class LifeMeter : MonoBehaviour
{
    public PlayerHealth playerHealth; // Referência ao script de saúde do jogador
    public Image healthBarImage; // Referência à imagem da barra de vida
    public Sprite[] healthSprites; // Array de sprites para diferentes estados de vida
    public int currentSpriteIndex;
    private void Start()
    {
        // Atualiza a barra de vida no início
        UpdateHealthBar();
    }

    private void Update()
    {
        // Atualiza a barra de vida sempre que a vida mudar
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
       var health = playerHealth.health / 10; 
        // Mapeia a vida do jogador para um índice de sprite
        int healthIndex = Mathf.Clamp(health, 0, healthSprites.Length - 1);
        Debug.Log(health);
        
        // Atualiza o sprite da barra de vida
        for (int i = currentSpriteIndex; i < healthIndex; i++)
        {
            healthBarImage.sprite = healthSprites[healthIndex];
        }
        currentSpriteIndex = healthIndex;
    }
}