using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El transform del jugador
    public Vector3 offset;   // El desplazamiento entre la cámara y el jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado

    void LateUpdate()
    {
        // Calculamos la posición deseada de la cámara (sin mover el eje Z)
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;
        
        // Suavizamos el movimiento de la cámara hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // Actualizamos la posición de la cámara
        transform.position = smoothedPosition;
    }
}
