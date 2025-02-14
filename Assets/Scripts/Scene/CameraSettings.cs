using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public float orthographicSize = 5f; // Tamaño ortográfico de la cámara
    public Vector3 targetOffset = new Vector3(0f, 0.3f, 0); // Posición en pantalla
    public bool ignoreY = true; 
    public BoxCollider2D boundingShape; 
}







