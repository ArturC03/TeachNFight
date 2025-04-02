using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPlayButtonClicked);
        gameObject.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        CharacterButton.StartGame();
    }
}
