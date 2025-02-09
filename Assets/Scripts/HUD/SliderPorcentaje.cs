using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderPorcentaje : MonoBehaviour
{
    private Slider slider;
    private TextMeshProUGUI porcentaje; // Donde se mostrará el porcentaje

    void Start()
    {
        // Buscar el Slider en el objeto padre
        slider = GetComponentInParent<Slider>(); // Busca el Slider en el objeto padre (o cualquier ancestro)
        
        // Si el Slider no se encuentra, mostramos un mensaje de error
        if (slider == null)
        {
            Debug.LogError("No se encontró el Slider en el objeto padre.");
            return;
        }

        porcentaje = GetComponent<TextMeshProUGUI>(); 

        // Si no se encuentra el componente TextMeshProUGUI, mostramos un error
        if (porcentaje == null)
        {
            Debug.LogError("No se encontró el componente TextMeshProUGUI en el objeto.");
            return;
        }
        
        // Inicializa el porcentaje
        UpdatePercentage();
        
        // Asigna el evento para actualizar el porcentaje cuando el valor del slider cambie
        slider.onValueChanged.AddListener(delegate { UpdatePercentage(); });
    }

    public void UpdatePercentage()
    {
        // Obtiene el valor actual del slider y lo utiliza directamente como porcentaje
        float percentage = slider.value;
        
        // Muestra el porcentaje en el TextMesh
        porcentaje.text = percentage.ToString("F0") + "%"; // Muestra el porcentaje sin decimales
    }
}
