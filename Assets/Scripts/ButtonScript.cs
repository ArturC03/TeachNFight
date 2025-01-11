using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Necessário para os eventos de pointer.

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite pressedSprite;   // Sprite que será mostrado enquanto o botão está pressionado.
    private Sprite originalSprite; // Sprite original que será restaurado.

    private Image buttonImage;     // Referência ao componente Image do botão.

    void Start()
    {
        // Obtém o componente Image do botão.
        buttonImage = GetComponent<Image>();

        // Verifica se o componente Image está configurado.
        if (buttonImage != null)
        {
            originalSprite = buttonImage.sprite; // Armazena o sprite original.
        }
        else
        {
            Debug.LogError("O componente Image não foi encontrado neste GameObject!");
        }
    }

    // Chamado quando o botão é pressionado.
    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonImage != null && pressedSprite != null)
        {
            buttonImage.sprite = pressedSprite;
        }
    }

    // Chamado quando o botão é solto.
    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonImage != null && originalSprite != null)
        {
            buttonImage.sprite = originalSprite;
        }
    }
}
