using UnityEngine;

public class SlowManager : MonoBehaviour
{
    public float slowMotionScale = 0.2f; // Velocidad en c�mara lenta
    public float normalTimeScale = 1f; // Velocidad normal
    private bool isCameraSlowed = false; // Verifica si la c�mara lenta est� activada
    
    [SerializeField] float slowMotionDuration; // Duraci�n del efecto de la c�mara lenta
    private float slowMotionTimer = 0f; // Temporizador para la duraci�n

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
        Debug.Log("C�mara lenta activada");
    }
    /*Desactivar la camara lenta*/
    private void DeactivateSlowMotion()
    {
        isCameraSlowed = false;
        Time.timeScale = normalTimeScale;
        Time.fixedDeltaTime = 0.02f;
        Debug.Log("C�mara lenta desactivada");
    }
}
