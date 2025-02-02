using UnityEngine;

public class SlowManager : MonoBehaviour
{
    public float slowMotionScale = 0.2f; // Velocidad en cámara lenta
    public float normalTimeScale = 1f; // Velocidad normal
    private bool isCameraSlowed = false; // Verifica si la cámara lenta está activada
    
    [SerializeField] float slowMotionDuration; // Duración del efecto de la cámara lenta
    private float slowMotionTimer = 0f; // Temporizador para la duración

    private void Update()
    {
        /*Controlamos que el jugador este presionando la tecla y el efecto de camara lenta este desactivado*/
        if (Input.GetKeyDown(KeyCode.E) && isCameraSlowed == false)
        {
            ActivateSlowMotion();
        }

        if (isCameraSlowed)
        {
            //Se restablece el tiempo de duracion de la camara con el unscaleDeltaTime
            slowMotionTimer -= Time.unscaledDeltaTime; 
            /*Si el tiempo llega a 0 o es menor desactivamos la camara lenta*/
            if (slowMotionTimer <= 0f)
            {
                DeactivateSlowMotion();
            }
        }
    }
    /*Activar la camara lenta*/
    private void ActivateSlowMotion()
    {
        isCameraSlowed = true;
        slowMotionTimer = slowMotionDuration;
        Time.timeScale = slowMotionScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajustamos FixedDeltaTime
        Debug.Log("Cámara lenta activada");
    }
    /*Desactivar la camara lenta*/
    private void DeactivateSlowMotion()
    {
        isCameraSlowed = false;
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f;
        Debug.Log("Cámara lenta desactivada");
    }
}
