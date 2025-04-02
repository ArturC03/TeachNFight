using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image buttonImage;
    public Sprite defaultSprite;
    public Sprite p1HoverSprite;
    public Sprite p2HoverSprite;
    public Sprite p1SelectedSprite;
    public Sprite p2SelectedSprite;

    // Character ID for identifying which character this button represents
    public string characterId;

    // Static variables for tracking game state
    private static bool isPlayer1SelectingNow = true;
    private static CharacterButton player1Selection = null;
    private static CharacterButton player2Selection = null;
    private static GameObject playButton;

    // UI Elements for player readiness
    public static GameObject player1ReadyIndicator;
    public static GameObject player2ReadyIndicator;

    // Player readiness status
    private static bool isPlayer1Ready = false;
    private static bool isPlayer2Ready = false;

    // Local state
    private bool isSelectedByPlayer1 = false;
    private bool isSelectedByPlayer2 = false;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();

        if (buttonImage && defaultSprite)
            buttonImage.sprite = defaultSprite;

        // Find UI elements if not assigned
        if (playButton == null)
            playButton = GameObject.Find("Canvas")?.transform.Find("PlayButton")?.gameObject;

        if (player1ReadyIndicator == null)
            player1ReadyIndicator = GameObject.FindWithTag("Player1Ready");

        if (player2ReadyIndicator == null)
            player2ReadyIndicator = GameObject.FindWithTag("Player2Ready");

        // Set initial UI states
        if (playButton != null)
            playButton.SetActive(false);

        if (player1ReadyIndicator != null)
            player1ReadyIndicator.SetActive(false);

        if (player2ReadyIndicator != null)
            player2ReadyIndicator.SetActive(false);
    }

    private void Update()
    {
        // Switch between players with Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchActivePlayer();
        }

        // Press Space to toggle ready status for current player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleReadyStatus();
        }
    }

    // Method to switch the active player
    public static void SwitchActivePlayer()
    {
        isPlayer1SelectingNow = !isPlayer1SelectingNow;
        Debug.Log("Now " + (isPlayer1SelectingNow ? "Player 1" : "Player 2") + " is selecting");
    }

    // Method to toggle ready status for current player
    private void ToggleReadyStatus()
    {
        if (isPlayer1SelectingNow)
        {
            if (player1Selection != null)
            {
                isPlayer1Ready = !isPlayer1Ready;
                if (player1ReadyIndicator != null)
                    player1ReadyIndicator.SetActive(isPlayer1Ready);

                Debug.Log("Player 1 is " + (isPlayer1Ready ? "ready" : "not ready"));
            }
            else
            {
                Debug.Log("Player 1 must select a character first!");
            }
        }
        else
        {
            if (player2Selection != null)
            {
                isPlayer2Ready = !isPlayer2Ready;
                if (player2ReadyIndicator != null)
                    player2ReadyIndicator.SetActive(isPlayer2Ready);

                Debug.Log("Player 2 is " + (isPlayer2Ready ? "ready" : "not ready"));
            }
            else
            {
                Debug.Log("Player 2 must select a character first!");
            }
        }

        // Check if both players are ready
        UpdatePlayButtonVisibility();
    }

    // Method to update play button visibility
    private static void UpdatePlayButtonVisibility()
    {
        if (playButton != null)
        {
            bool bothPlayersReady = (isPlayer1Ready && isPlayer2Ready);
            playButton.SetActive(bothPlayersReady);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Only highlight if not already selected by current player
        if ((isPlayer1SelectingNow && isSelectedByPlayer1) || (!isPlayer1SelectingNow && isSelectedByPlayer2))
            return;

        // Change hover sprite based on current player
        if (isPlayer1SelectingNow && buttonImage && p1HoverSprite)
            buttonImage.sprite = p1HoverSprite;
        else if (!isPlayer1SelectingNow && buttonImage && p2HoverSprite)
            buttonImage.sprite = p2HoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        // Priority: P1 selected > P2 selected > default
        if (isSelectedByPlayer1 && buttonImage && p1SelectedSprite)
        {
            buttonImage.sprite = p1SelectedSprite;
        }
        else if (isSelectedByPlayer2 && buttonImage && p2SelectedSprite)
        {
            buttonImage.sprite = p2SelectedSprite;
        }
        else if (!isSelectedByPlayer1 && !isSelectedByPlayer2 && buttonImage && defaultSprite)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Reset readiness when changing selection
        if (isPlayer1SelectingNow)
        {
            // Only change selection if player is not ready or changing to new character
            if (!isPlayer1Ready || player1Selection != this)
            {
                // Reset player ready status
                isPlayer1Ready = false;
                if (player1ReadyIndicator != null)
                    player1ReadyIndicator.SetActive(false);

                // Deselect previous player 1 selection if exists
                if (player1Selection != null && player1Selection != this)
                {
                    player1Selection.isSelectedByPlayer1 = false;
                    player1Selection.UpdateButtonSprite();
                }

                // Set this as the new player 1 selection
                player1Selection = this;
                isSelectedByPlayer1 = true;

                Debug.Log($"Player 1 selected character: {gameObject.name} (ID: {characterId})");
            }
        }
        else
        {
            // Only change selection if player is not ready or changing to new character
            if (!isPlayer2Ready || player2Selection != this)
            {
                // Reset player ready status
                isPlayer2Ready = false;
                if (player2ReadyIndicator != null)
                    player2ReadyIndicator.SetActive(false);

                // Deselect previous player 2 selection if exists
                if (player2Selection != null && player2Selection != this)
                {
                    player2Selection.isSelectedByPlayer2 = false;
                    player2Selection.UpdateButtonSprite();
                }

                // Set this as the new player 2 selection
                player2Selection = this;
                isSelectedByPlayer2 = true;

                Debug.Log($"Player 2 selected character: {gameObject.name} (ID: {characterId})");
            }
        }

        // Update the button sprite based on selection state
        UpdateButtonSprite();

        // Update play button visibility
        UpdatePlayButtonVisibility();
    }

    // Method to be called by the Play button
    public static void StartGame()
    {
        if (player1Selection != null && player2Selection != null && isPlayer1Ready && isPlayer2Ready)
        {
            // Store the selected character IDs in PlayerPrefs for retrieval in the next scene
            PlayerPrefs.SetString("Player1Character", player1Selection.characterId);
            PlayerPrefs.SetString("Player2Character", player2Selection.characterId);
            PlayerPrefs.Save();

            // Load the gameplay scene
            SceneManager.LoadScene("luta"); // Replace with your actual scene name
        }
    }

    // Method to reset everything
    public static void ResetAll()
    {
        if (player1Selection != null)
        {
            player1Selection.isSelectedByPlayer1 = false;
            player1Selection.UpdateButtonSprite();
            player1Selection = null;
        }

        if (player2Selection != null)
        {
            player2Selection.isSelectedByPlayer2 = false;
            player2Selection.UpdateButtonSprite();
            player2Selection = null;
        }

        isPlayer1Ready = false;
        isPlayer2Ready = false;
        isPlayer1SelectingNow = true;

        if (player1ReadyIndicator != null)
            player1ReadyIndicator.SetActive(false);

        if (player2ReadyIndicator != null)
            player2ReadyIndicator.SetActive(false);

        if (playButton != null)
            playButton.SetActive(false);
    }
}
