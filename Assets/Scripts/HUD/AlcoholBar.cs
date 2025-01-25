using System.Collections;
using UnityEngine;

public class AlcoholBar : MonoBehaviour
{
     private int bubblesCant = 10;  // Número inicial de burbujas
    private const int MaxBubbles = 10;  // Máximo de burbujas
    private const int MinBubbles = 1;  // Mínimo de burbujas
    
    [SerializeField] private GameObject[] bubbles;

    private void Update()
    {
        // Con R, agrega 3 burbujas
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(AgregarBurbujas(3));
        }
        
        // Con T, agrega 5 burbujas
        else if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(AgregarBurbujas(5));
        }
        
        // Con Espacio, agrega 1 burbuja
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AgregarBurbujas(1));
        }
        
        // Con F1, quita 1 burbuja
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            PerderBubble();
        }
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
    }

    // Agregar burbujas segun la cantidad
    public IEnumerator AgregarBurbujas(int cantidad)
    {
        if (bubblesCant < MaxBubbles)
        {
            int newBubbles = Mathf.Min(bubblesCant + cantidad, MaxBubbles);
            for (int i = bubblesCant; i < newBubbles; i++)
            {
                yield return new WaitForSeconds(0.3f);
                ActivarBubble(i);
            }
            bubblesCant = newBubbles;
            Debug.Log($"Agregaste {cantidad} burbujas, ahora hay {bubblesCant}");

            // Si alcanzamos el máximo, comenzamos a quitar las burbujas una por una
            if (bubblesCant == MaxBubbles)
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(QuitarBurbujasUnaPorUna());
            }
        }
    }

    // Quitar burbujas segun la cantidad
    public IEnumerator QuitarBurbujasUnaPorUna()
    {
            
        while (bubblesCant > MinBubbles)
        {
            yield return new WaitForSeconds(0.2f);
            bubblesCant--;
            DesactivarBubble(bubblesCant);
            Debug.Log($"Quitaste una burbuja, ahora hay {bubblesCant} burbujas.");
        }
        
    }

    // public void DesactivarHUD(){
    //     this.gameObject.SetActive(false);
    // }
}
