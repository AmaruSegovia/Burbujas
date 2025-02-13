using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    [SerializeField] private float timeContador;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI mensajeText;
    private bool isCountdownActive = false; // Verifica si el contador está activo

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

        while (countdownTime >= 0)
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

        if (countDownText != null)
        {
            countDownText.text = "00:000";
        }

        // Cuando el tiempo llega a 0, cargar la escena de GameOver
        if (countDownText != null)
        {
            mensajeText.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(4f);
        }

        SceneManager.LoadScene("GameOver");
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
