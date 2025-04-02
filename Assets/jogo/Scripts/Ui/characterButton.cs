using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image buttonImage; // Reference to the button's Image component
    public Sprite defaultSprite;   // Default sprite (unchanged)
    public Sprite p1HoverSprite;   // Player 1 hover sprite
    public Sprite p2HoverSprite;   // Player 2 hover sprite
    public Sprite p1SelectedSprite; // Player 1 selected sprite (can be the same as hover)
    public Sprite p2SelectedSprite; // Player 2 selected sprite (can be the same as hover)

    // Static variables to track game state across all buttons
    private static bool isPlayer1Turn = true; // Track which player's turn it is
    private static CharacterButton player1Selection = null;
    private static CharacterButton player2Selection = null;

    // Local state
    private bool isSelected = false;
    private bool isPlayer1Button = false;

    private void Awake()
    {
        buttonImage = GetComponent<Image>(); // Get the Image component from this button

        // Make sure the button starts with the default sprite
        if (buttonImage && defaultSprite)
            buttonImage.sprite = defaultSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected) return; // Don't change sprite if the button is already selected

        // Change sprite based on the player selecting
        if (isPlayer1Turn && buttonImage && p1HoverSprite)
            buttonImage.sprite = p1HoverSprite; // Show Player 1's hover sprite
        else if (!isPlayer1Turn && buttonImage && p2HoverSprite)
            buttonImage.sprite = p2HoverSprite; // Show Player 2's hover sprite
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelected) return; // Don't change sprite if the button is already selected

        if (buttonImage && defaultSprite)
            buttonImage.sprite = defaultSprite; // Revert to default sprite when hover ends
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected) return; // If already selected, don't do anything

        // Handle selection based on current player
        if (isPlayer1Turn)
        {
            // Deselect previous player 1 selection if exists
            if (player1Selection != null)
            {
                player1Selection.isSelected = false;
                player1Selection.buttonImage.sprite = player1Selection.defaultSprite;
            }

            // Set this as the new player 1 selection
            player1Selection = this;
            isPlayer1Button = true;

            // Apply selected sprite
            if (buttonImage && p1SelectedSprite)
                buttonImage.sprite = p1SelectedSprite;
            else if (buttonImage && p1HoverSprite)
                buttonImage.sprite = p1HoverSprite;
        }
        else
        {
            // Deselect previous player 2 selection if exists
            if (player2Selection != null)
            {
                player2Selection.isSelected = false;
                player2Selection.buttonImage.sprite = player2Selection.defaultSprite;
            }

            // Set this as the new player 2 selection
            player2Selection = this;
            isPlayer1Button = false;

            // Apply selected sprite
            if (buttonImage && p2SelectedSprite)
                buttonImage.sprite = p2SelectedSprite;
            else if (buttonImage && p2HoverSprite)
                buttonImage.sprite = p2HoverSprite;
        }

        isSelected = true; // Mark this button as selected

        // Switch to the next player's turn
        isPlayer1Turn = !isPlayer1Turn;

        // You might want to add an event to notify other parts of your game
        // that a character has been selected
        Debug.Log($"Player {(isPlayer1Button ? "1" : "2")} selected character: {gameObject.name}");
    }

    // Optional: Public method to reset all selections (could be called from a game manager)
    public static void ResetSelections()
    {
        if (player1Selection != null)
        {
            player1Selection.isSelected = false;
            player1Selection.buttonImage.sprite = player1Selection.defaultSprite;
            player1Selection = null;
        }

        if (player2Selection != null)
        {
            player2Selection.isSelected = false;
            player2Selection.buttonImage.sprite = player2Selection.defaultSprite;
            player2Selection = null;
        }

        isPlayer1Turn = true;
    }
}
