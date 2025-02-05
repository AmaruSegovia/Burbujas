using System.Collections; // Agregado para las corutinas
using UnityEngine;
using UnityEngine.UI;

public class AlcoholBar02 : MonoBehaviour
{
    public static AlcoholBar02 Instance { get; private set ;} 

    private Slider slider;
    public float alcoholMax = 10; 
    [SerializeField] private float alcoholActual = 1;

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

    void Start()
    {
        slider = GetComponent<Slider>();
        InicializarBarraAlcohol();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Al presionar R, quita todo el alcohol actual con animación
            QuitarAlcohol(0.5f, 1.5f); // 0.5f: tiempo de espera, 1.5f: duración de la animación
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Al presionar R, quita todo el alcohol actual con animación
            QuitarAlcohol(3f, 0.5f, 1.5f); // 3f: cantidad de alcohol a quitar, 0.5f: tiempo de espera, 1.5f: duración de la animación
        }
    }

    public void CambiarMaxAlcohol(float MaxAlcohol){
        slider.maxValue = MaxAlcohol;
    }

    public void CambiarNivelAlcohol(float cantAlcohol){
        slider.value = cantAlcohol;
    }

    public void InicializarBarraAlcohol(){
        CambiarMaxAlcohol(alcoholMax);
        CambiarNivelAlcohol(alcoholActual);
    }

    // ----- Metodos para Cambiar el valor actual del alcohol 

    
    // Método para agregar alcohol
    public void AgregarAlcohol(float alcoholValue)
    {
        alcoholActual+=alcoholValue;
        CambiarNivelAlcohol(alcoholActual);
    }
    // Método para agregar X cantidad de alcohol con animación
    public void AgregarAlcohol(float alcoholValue, float timeEspera, float timeAnimation)
    {
        StartCoroutine(ActualizarBarraDeAlcoholAnimado(alcoholValue, timeEspera, timeAnimation));
    }

    // Método para quitar X canitdad de alcohol con animación
    public void QuitarAlcohol(float alcoholValue, float timeEspera, float timeAnimation)
    {
        StartCoroutine(ActualizarBarraDeAlcoholAnimado(-alcoholValue, timeEspera, timeAnimation));
    }

    // Método para quitar alcohol por unidades con animación
    public void QuitarAlcohol(float timeEspera, float timeAnimation)
    {
        StartCoroutine(ActualizarBarraDeAlcoholAnimado(-alcoholActual, timeEspera, timeAnimation));
    }

    // Corutina que anima el valor del slider
    private IEnumerator ActualizarBarraDeAlcoholAnimado(float alcoholValue, float tiempoEspera, float duracionAnimation)
    {
        float valorInicial = slider.value; // Valor actual del slider
        float valorFinal = Mathf.Clamp(alcoholActual + alcoholValue, 0, slider.maxValue); // Valor final con el alcohol agregado
        float tiempoTranscurrido = 0f;

        yield return new WaitForSeconds(tiempoEspera);

        while (tiempoTranscurrido < duracionAnimation)
        {
            tiempoTranscurrido += Time.deltaTime;
            slider.value = Mathf.Lerp(valorInicial, valorFinal, tiempoTranscurrido / duracionAnimation); // Interpolación del valor

            yield return null; // Esperar hasta el siguiente frame
        }

        // Asegurarse de que el valor final se ajuste al valor esperado
        alcoholActual = valorFinal;
        Debug.Log("canitdad actual: "+alcoholActual);
        CambiarNivelAlcohol(alcoholActual);
    }
}
