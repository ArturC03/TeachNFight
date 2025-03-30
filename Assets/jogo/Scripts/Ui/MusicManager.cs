using UnityEngine; 
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // Instância Singleton
    public bool isMusicOn = true; // Variável para controlar o estado da música
    private AudioSource audioSource; // Referência ao componente de áudio

    private const string MusicPreferenceKey = "isMusicOn"; // Chave para guardar a preferência de música no PlayerPrefs

    private void Awake()
    {
        // Garante que há apenas uma instância do MusicManager
        if (Instance == null)
        {
            Instance = this; // Definir a instância para esta cópia
            DontDestroyOnLoad(gameObject); // Impede que o objeto seja destruído quando mudar de cena
        }
        else
        {
            Destroy(gameObject); // Destroi qualquer instância duplicada
        }

        audioSource = GetComponent<AudioSource>(); // Obtém o AudioSource

        // Carregar a preferência de música do PlayerPrefs
        if (PlayerPrefs.HasKey(MusicPreferenceKey))
        {
            isMusicOn = PlayerPrefs.GetInt(MusicPreferenceKey) == 1; // Se o valor for 1, a música está ligada
        }

        // Tocar ou pausar música de acordo com o estado
        if (isMusicOn && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Método para alternar o estado da música
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
        {
            audioSource.Play(); // Toca a música
        }
        else
        {
            audioSource.Pause(); // Pausa a música
        }

        // Salva a preferência no PlayerPrefs
        PlayerPrefs.SetInt(MusicPreferenceKey, isMusicOn ? 1 : 0);
        PlayerPrefs.Save(); // Garante que os dados são realmente salvos
    }
}