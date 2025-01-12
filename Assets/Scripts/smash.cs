using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform[] players; // Lista de Transform dos jogadores
    public float minZoom = 5f;  // Zoom mínimo (câmara mais aproximada)
    public float maxZoom = 20f; // Zoom máximo (câmara mais afastada)
    public float zoomLimiter = 50f; // Controla a sensibilidade do zoom
    public Vector2 mapSize; // Tamanho do mapa (largura e altura)
    public Vector2 mapCenter; // Centro do mapa (posição no mundo)

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (players.Length == 0) return;

        MoveCamera();
        ZoomCamera();
        ClampCameraPosition();
    }

    void MoveCamera()
    {
        Vector3 centerPoint = GetCenterPoint();
        centerPoint.z = -10f; // Mantém a câmara no plano 2D
        transform.position = centerPoint;
    }

    void ZoomCamera()
    {
        float maxDistance = GetGreatestDistance();
        float newZoom = Mathf.Lerp(maxZoom, minZoom, maxDistance / zoomLimiter); // Zoom ajustado para o comportamento correto
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom); // Limitar ao intervalo permitido

        // Evitar mostrar áreas fora do mapa
        float halfMapHeight = mapSize.y / 2;
        float halfMapWidth = mapSize.x / 2 / cam.aspect;

        newZoom = Mathf.Min(newZoom, halfMapHeight); // Garante que o zoom não ultrapassa os limites do mapa
        cam.orthographicSize = newZoom;
    }

    void ClampCameraPosition()
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        // Ajusta os limites com base no centro do mapa
        float minX = mapCenter.x - mapSize.x / 2 + halfWidth;
        float maxX = mapCenter.x + mapSize.x / 2 - halfWidth;
        float minY = mapCenter.y - mapSize.y / 2 + halfHeight;
        float maxY = mapCenter.y + mapSize.y / 2 - halfHeight;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        transform.position = clampedPosition;
    }

    Vector3 GetCenterPoint()
    {
        if (players.Length == 1)
        {
            return players[0].position;
        }

        var bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform player in players)
        {
            bounds.Encapsulate(player.position);
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform player in players)
        {
            bounds.Encapsulate(player.position);
        }

        return bounds.size.x > bounds.size.y ? bounds.size.x : bounds.size.y;
    }
}
