using UnityEngine;

public class ChangeCameraBackground : MonoBehaviour
{
    public Color newBackgroundColor = Color.blue; // Color de fondo deseado (puedes elegir cualquier color)

    void Start()
    {
        // Cambiar el color de fondo de la cámara
        Camera.main.backgroundColor = newBackgroundColor;
    }
}
