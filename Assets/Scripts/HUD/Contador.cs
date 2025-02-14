using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    public static Contador instance { get; private set; }
    [SerializeField] private float timeContador = 10f;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI mensajeText;
    private bool isCountdownActive = false; // Verifica si el contador está activo
    private string pausedTime = ""; // Almacena el tiempo donde se detuvo el contador
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Mas de un Contador en escena");
        }
    }
    void Start()
    {
        if (countDownText != null)
        {
            countDownText.text = "";
        }
    }

    private IEnumerator CountdownToGameOver()
    {
        isCountdownActive = true;
        float countdownTime = timeContador ;

        while (countdownTime >= 0 && isCountdownActive)
        {
            // Calcula segundos y milisegundos
            int seconds = Mathf.FloorToInt(countdownTime);
            int milliseconds = Mathf.FloorToInt((countdownTime % 1f) * 1000f);

            // Actualiza el texto del contador
            if (countDownText != null)
            {
                countDownText.text = $"{seconds:D2}:{milliseconds:D3}";
            }

            // Espera un segundo antes de decrementar
            yield return null;
            countdownTime -= Time.deltaTime;
        }
         // Si el contador llega a 0, mostramos el mensaje y cargamos el GameOver
        if(countdownTime <= 0){
            if (countDownText != null)
            {
                countDownText.text = "00:000";
            }

            mensajeText.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(4f);
            SceneManager.LoadScene("GameOver");
            isCountdownActive = false;
        }
    }

    public void DetenerYParpadearContador(int cantidadDeParpadeos)
    {
        // Guardamos el tiempo donde se detuvo el contador
        pausedTime = countDownText.text;
        // Detener cualquier corrutina en este script
        //StopAllCoroutines();

        // Comenzamos el parpadeo con la cantidad deseada
        StartCoroutine(ParpadearContador(cantidadDeParpadeos)); 
        isCountdownActive = false;
    }

    private IEnumerator ParpadearContador(int cantidadDeParpadeos)
    {
        int parpadeosRestantes = cantidadDeParpadeos; // Guardamos cuántas veces más debe parpadear
        while (parpadeosRestantes > 0)
        {
            // Establece el texto vacío
            countDownText.text = "";
            yield return new WaitForSeconds(0.3f); // 0.5 segundos sin texto

            // Vuelve a mostrar el texto original
            countDownText.text = pausedTime;
            yield return new WaitForSeconds(0.5f); // 0.5 segundos con texto

            // Decrementamos el número de parpadeos restantes
            parpadeosRestantes--;
        }

        // Finalmente, el contador vuelve a su estado normal después de los parpadeos
        countDownText.text = "";  // O el valor que tenía antes
    }


    // Metodo para iniciar el contador
    public void StartCountdown()
    {
        if (!isCountdownActive)
        {
            StartCoroutine(CountdownToGameOver());
        }
    }
}
