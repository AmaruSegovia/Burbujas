using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Header("Parámetros de la cámara")]
    public Transform player; // El transform del jugador
    public Vector3 offset;   // El desplazamiento entre la cámara y el jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado

    [Header("Limites del mapa")]
    // Limites para el mapa
    public float minX;
    public float maxX, minY, maxY;

    void LateUpdate()
    {
        // Calculamos los límites visibles de la cámara
        float cameraHeight = Camera.main.orthographicSize * 2;  // El alto visible de la cámara
        float cameraWidth = cameraHeight * Camera.main.aspect;  // El ancho visible basado en la relación de aspecto

        // Calculamos la posición deseada de la cámara en función del jugador y el offset
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;

        // Suavizamos el movimiento de la cámara hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aseguramos que la cámara no se salga de los límites del mapa
        float clampedX = Mathf.Clamp(smoothedPosition.x, minX + cameraWidth / 2, maxX - cameraWidth / 2);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY + cameraHeight / 2, maxY - cameraHeight / 2);

        // Aplica la posición limitada a la cámara
        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }

    // Método para dibujar las líneas de los límites en la ventana de la escena
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Color de las líneas

        // Dibujamos las líneas de los límites basados en los valores de minX, maxX, minY, maxY
        Gizmos.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0)); // Línea inferior
        Gizmos.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0)); // Línea derecha
        Gizmos.DrawLine(new Vector3(maxX, maxY, 0), new Vector3(minX, maxY, 0)); // Línea superior
        Gizmos.DrawLine(new Vector3(minX, maxY, 0), new Vector3(minX, minY, 0)); // Línea izquierda
    }
}
