using UnityEngine;

public class FightingManger : MonoBehaviour
{
    public GameObject[] allCharactersInScene;

    private dinamiccamera cameraScript;

    void Start()
    {
        cameraScript = Camera.main.GetComponent<dinamiccamera>();

        string player1Character = PlayerPrefs.GetString("Player1Character", "0");
        string player2Character = PlayerPrefs.GetString("Player2Character", "1");

        Debug.Log("[FightingManager] Player 1 selected: " + player1Character);
        Debug.Log("[FightingManager] Player 2 selected: " + player2Character);

        ActivateChosenCharacters(player1Character, player2Character);
    }

    void ActivateChosenCharacters(string p1CharacterId, string p2CharacterId)
    {
        int p1Id = int.Parse(p1CharacterId);
        int p2Id = int.Parse(p2CharacterId);

        string[] names = { "MJC", "Rubem", "alda", "avelino", "helder" };

        GameObject p1 = null, p2 = null;

        foreach (var character in allCharactersInScene)
        {
            string lowerName = character.name.ToLower();

            Debug.Log("[FightingManager] Checking character in scene: " + character.name);

            if (lowerName == names[p1Id].ToLower())
            {
                character.SetActive(true);
                var combat = character.GetComponent<PlayerCombat>();
                if (combat != null)
                {
                    combat.player = 1;
                    Debug.Log($"[FightingManager] Player 1 character '{character.name}' activated and assigned PlayerCombat.player = 1");
                }
                else
                {
                    Debug.LogWarning($"[FightingManager] Player 1 character '{character.name}' has no PlayerCombat script!");
                }
                p1 = character;
            }
            else if (lowerName == names[p2Id].ToLower())
            {
                character.SetActive(true);
                var combat = character.GetComponent<PlayerCombat>();
                if (combat != null)
                {
                    combat.player = 2;
                    Debug.Log($"[FightingManager] Player 2 character '{character.name}' activated and assigned PlayerCombat.player = 2");
                }
                else
                {
                    Debug.LogWarning($"[FightingManager] Player 2 character '{character.name}' has no PlayerCombat script!");
                }
                p2 = character;
            }
            else
            {
                character.SetActive(false);
                Debug.Log($"[FightingManager] Character '{character.name}' disabled (not selected)");
            }
        }

        if (cameraScript != null)
        {
            cameraScript.players = new Transform[] { p1?.transform, p2?.transform };
            Debug.Log("[FightingManager] Camera script updated with selected characters.");
        }
        else
        {
            Debug.LogError("[FightingManager] dinamiccamera script not found on Main Camera!");
        }
    }
}
