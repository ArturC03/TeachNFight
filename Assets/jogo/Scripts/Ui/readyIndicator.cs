using UnityEngine;

public class PlayerReadyIndicator : MonoBehaviour
{
public bool isPlayer1;
    
    void Start()
    {
        if (isPlayer1)
            CharacterButton.player1ReadyIndicator = gameObject;
        else
            CharacterButton.player2ReadyIndicator = gameObject;
            
        gameObject.SetActive(false);
    }
}
