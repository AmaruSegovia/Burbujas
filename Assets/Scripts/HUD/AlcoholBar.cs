using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AlcoholBar : MonoBehaviour
{
    public static AlcoholBar Instance { get; private set ;} 

    public int bubblesCant = 4;  // Número inicial de burbujas
    public const int MaxBubbles = 10;  // Máximo de burbujas
    public const int MinBubbles = 1;  // Mínimo de burbujas
    
    [SerializeField] private GameObject[] bubbles;
    [SerializeField] private float tiempoParaQuitarBurbuja;
    [SerializeField] private float tiempoParaAgregarBurbuja;

    [SerializeField] private float timeContador;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI mensajeText;
    private bool isCountdownActive = false; // Verifica si el contador está activo

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start() {
        InicializarBurbujas();

        if (countDownText != null)
        {
            countDownText.text = "";
        }
    }

    private void Update()
    {
        // Con F1, quita 1 burbuja ( para depurar)
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PerderBubble();
        }

        // Con espacio, quita 1 burbuja ( para depurar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AgregarBurbujas(6));
        }
        // Codigo para agregar burbujas desde otro script
        // StartCoroutine(AlcoholBar.Instance.AgregarBurbujas(4));
    }

    // Activar una burbuja
    public void ActivarBubble(int i)
    {
        if (i >= 0 && i < bubbles.Length) 
        {
            bubbles[i].SetActive(true);
        }
    }

    // Desactivar una burbuja
    public void DesactivarBubble(int i)
    {
        if (i >= 0 && i < bubbles.Length) 
        {
            bubbles[i].SetActive(false);
        }
    }

    // Metodo para perder Burbujas
    public void PerderBubble()
    {
        if (bubblesCant > MinBubbles)
        {
            bubblesCant--;
            DesactivarBubble(bubblesCant);
        }

        // Detener el contador si las burbujas ya no están al máximo
        if (isCountdownActive && bubblesCant < MaxBubbles)
        {
            StopCoroutine(CountdownToGameOver());
            isCountdownActive = false;

            // Limpiar el texto del contador
                if (countDownText != null)
                {
                    countDownText.text = "";
                }

            Debug.Log("Contador cancelado: las burbujas ya no están al máximo.");
        }
    }

    public void beber(int cant){
        StartCoroutine(AgregarBurbujas(cant));
    }

    // Agregar burbujas segun la cantidad
    public IEnumerator AgregarBurbujas(int cantidad)
    {
        if (bubblesCant < MaxBubbles)
        {
            int newBubbles = Mathf.Min(bubblesCant + cantidad, MaxBubbles);
            yield return new WaitForSeconds(1f);
            for (int i = bubblesCant; i < newBubbles; i++)
            {
                yield return new WaitForSeconds(tiempoParaAgregarBurbuja);
                ActivarBubble(i);
            }
            bubblesCant = newBubbles;
            Debug.Log($"Agregaste {cantidad} burbujas, ahora hay {bubblesCant}");

            // Si alcanzo el máximo, comenzamos a quitar las burbujas una por una
            // if (bubblesCant == MaxBubbles)
            // {
            //     yield return new WaitForSeconds(1f);
            //     StartCoroutine(QuitarBurbujasUnaPorUna());
            // }

            // Si alcanzo el maximo de burbujas, iniciar contador
            if (bubblesCant == MaxBubbles && !isCountdownActive)
            {
                StartCoroutine(CountdownToGameOver());
            }
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

    // Quitar burbujas segun la cantidad
    public IEnumerator QuitarBurbujasUnaPorUna()
    {
        while (bubblesCant > MinBubbles)
        {
            yield return new WaitForSeconds(tiempoParaQuitarBurbuja);
            bubblesCant--;
            DesactivarBubble(bubblesCant);
            Debug.Log($"Quitaste una burbuja, ahora hay {bubblesCant} burbujas.");
        }
        
    }

    // public void DesactivarHUD(){
    //     this.gameObject.SetActive(false);
    // }

    public void InicializarBurbujas(){

        for (int i = 0; i < bubblesCant; i++)
        {
            ActivarBubble(i); // Activa cada burbuja hasta "bubblesCant"
        }

        for (int i = bubblesCant; i < bubbles.Length; i++)
        {
            DesactivarBubble(i); // Asegura que las burbujas restantes esten desactivadas
        }
    }
}
