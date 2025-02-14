using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public float orthographicSize = 5f; // Tama�o ortogr�fico de la c�mara
    public Vector3 targetOffset = new Vector3(0f, 0.3f, 0); // Posici�n en pantalla
    public bool ignoreY = true; 
    public BoxCollider2D boundingShape; 
}







