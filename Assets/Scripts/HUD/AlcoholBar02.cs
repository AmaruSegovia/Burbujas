using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AlcoholBar02 : MonoBehaviour
{
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void CambiarMaxAlcohol(float MaxAlcohol){
        slider.maxValue = MaxAlcohol;
    }

    public void CambiarNivelAlcohol(float cantAlcohol){
        slider.value = cantAlcohol;
    }

    public void InicializarBarraAlcohol(float alcoholValue, float alcoholMax){
        CambiarMaxAlcohol(alcoholMax);
        CambiarNivelAlcohol(alcoholValue);
    }

    // Corutina que anima el valor del slider
    public IEnumerator ActualizarBarraDeAlcoholAnimado(float alcoholValue, float tiempoEspera, float duracionAnimation, System.Action onComplete)
    {
        float valorInicial = slider.value; // Valor actual del slider
        float valorFinal = Mathf.Clamp(valorInicial + alcoholValue, 0, slider.maxValue); // Valor final con el alcohol agregado
        float tiempoTranscurrido = 0f;

        yield return new WaitForSeconds(tiempoEspera);

        while (tiempoTranscurrido < duracionAnimation)
        {
            tiempoTranscurrido += Time.deltaTime;
            slider.value = Mathf.Lerp(valorInicial, valorFinal, tiempoTranscurrido / duracionAnimation); // Interpolación del valor

            yield return null; // Esperar hasta el siguiente frame
        }
        // Ejecutar el callback cuando termine la corutina
        onComplete?.Invoke();
    }
}
