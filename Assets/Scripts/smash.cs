using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public Transform[] players; // Lista de Transform dos jogadores
    public float minZoom = 5f;  // Zoom m�nimo (c�mara mais aproximada)
    public float maxZoom = 20f; // Zoom m�ximo (c�mara mais afastada)
    public float zoomLimiter = 50f; // Controla a sensibilidade do zoom
    public Vector2 mapBounds; // Dimens�o do mapa (x, y)

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
    }

    void MoveCamera()
    {
        Vector3 centerPoint = GetCenterPoint();
        centerPoint.z = -10f; // Mant�m a c�mara no plano 2D
        centerPoint.x = Mathf.Clamp(centerPoint.x, -mapBounds.x / 2, mapBounds.x / 2);
        centerPoint.y = Mathf.Clamp(centerPoint.y, -mapBounds.y / 2, mapBounds.y / 2);

        transform.position = centerPoint;
    }

    void ZoomCamera()
    {
        float maxDistance = GetGreatestDistance();
        float newZoom = Mathf.Lerp(minZoom, maxZoom, maxDistance / zoomLimiter);
        cam.orthographicSize = Mathf.Clamp(newZoom, minZoom, maxZoom);
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
            if (player != null)
            bounds.Encapsulate(player.position);
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform player in players)
        {
            if (player != null)
            bounds.Encapsulate(player.position);
        }

        return bounds.size.x > bounds.size.y ? bounds.size.x : bounds.size.y;
    }
}
