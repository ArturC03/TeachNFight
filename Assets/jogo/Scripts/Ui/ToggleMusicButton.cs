using UnityEngine;
using UnityEngine.UI; // Necessário para o componente Image

public class ToggleMusicButton : MonoBehaviour
{
    public Sprite musicOnSprite; // Imagem para som ligado
    public Sprite musicOffSprite; // Imagem para som desligado
    private Image buttonImage; // Componente Image do botão

    private void Start()
    {
        // Obter o componente Image do botão
        buttonImage = GetComponent<Image>();

        // Atualizar a imagem inicial com base no estado da música
        UpdateButtonImage();
    }

    public void OnToggleMusic()
    {
        // Alternar o estado da música no MusicManager
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.ToggleMusic();
            UpdateButtonImage(); // Atualizar a imagem do botão
        }
    }

    private void UpdateButtonImage()
    {
        // Troca a imagem com base no estado da música
        if (MusicManager.Instance != null && MusicManager.Instance.isMusicOn)
        {
            buttonImage.sprite = musicOnSprite; // Define o sprite para som ligado
        }
        else
        {
            buttonImage.sprite = musicOffSprite; // Define o sprite para som desligado
        }
    }
}