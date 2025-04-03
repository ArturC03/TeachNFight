using UnityEngine;

public class FightingManger : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Prefabs dos personagens

    private dinamiccamera cameraScript; // Referência para o script da câmera

    void Start()
    {
        cameraScript = Camera.main.GetComponent<dinamiccamera>(); // Obtém o script da câmera

        string player1Character = PlayerPrefs.GetString("Player1Character", "DefaultCharacter1");
        string player2Character = PlayerPrefs.GetString("Player2Character", "DefaultCharacter2");

        Debug.Log("Player 1 selected: " + player1Character);
        Debug.Log("Player 2 selected: " + player2Character);

        SpawnCharacters(player1Character, player2Character);
    }

    void SpawnCharacters(string p1Character, string p2Character)
    {
        GameObject p1Prefab = GetCharacterPrefab(p1Character);
        GameObject p2Prefab = GetCharacterPrefab(p2Character);

        GameObject p1 = null, p2 = null;

        if (p1Prefab != null)
            p1 = Instantiate(p1Prefab, new Vector3(-2, 0, 0), Quaternion.identity);

        if (p2Prefab != null)
            p2 = Instantiate(p2Prefab, new Vector3(2, 0, 0), Quaternion.identity);

        // 🟢 Adiciona os jogadores à câmera
        if (cameraScript != null)
        {
            cameraScript.players = new Transform[] { p1?.transform, p2?.transform };
        }
        else
        {
            Debug.LogError("dinamiccamera script not found on Main Camera!");
        }
    }

    GameObject GetCharacterPrefab(string characterId)
    {
        int id = int.Parse(characterId);
        string[] osama = { "MJC", "Rubem", "alda", "avelino", "helder" };

        foreach (var character in characterPrefabs)
        {
            if (character.name.ToLower() == osama[id].ToLower())
                return character;
        }

        Debug.LogError("Character not found: " + characterId);
        return null;
    }
}
