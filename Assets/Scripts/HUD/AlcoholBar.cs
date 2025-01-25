using System.Collections;
using UnityEngine;


public class AlcoholBar : MonoBehaviour
{
    public static AlcoholBar Instance { get; private set ;} 

    public int bubblesCant = 4;  // Número inicial de burbujas
    public const int MaxBubbles = 10;  // Máximo de burbujas
    public const int MinBubbles = 1;  // Mínimo de burbujas
    
    [SerializeField] private GameObject[] bubbles;
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
    }

    private void Update()
    {
        // Con R, agrega 3 burbujas
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(AgregarBurbujas(3));
        }
        
        // Con F1, quita 1 burbuja
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            PerderBubble();
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
            for (int i = bubblesCant; i < newBubbles; i++)
            {
                yield return new WaitForSeconds(0.2f);
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
