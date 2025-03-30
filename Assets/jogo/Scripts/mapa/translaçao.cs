using UnityEngine;

public class PlatformPingPongList : MonoBehaviour
{
    public Transform[] points; // Lista de pontos pelo qual a plataforma vai passar
    public float speed = 2f;   // Velocidade do movimento
    private int currentPointIndex = 0; // Índice do ponto atual

    void Update()
    {
        if (points.Length < 2)
        {
            Debug.LogWarning("Adiciona pelo menos dois pontos para o movimento funcionar!");
            return;
        }

        // Move a plataforma em direção ao ponto atual
        Transform targetPoint = points[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Verifica se chegou ao ponto atual
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Avança para o próximo ponto ou volta ao início
            currentPointIndex++;
            if (currentPointIndex >= points.Length)
                currentPointIndex = 0; // Reinicia o ciclo
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jogador entrou na plataforma
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Torna o jogador filho da plataforma
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Verifica se o jogador saiu da plataforma
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); // Remove o jogador da plataforma
        }
    }
}
