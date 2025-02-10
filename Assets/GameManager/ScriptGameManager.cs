using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptGameManager : MonoBehaviour
{
    /*Esto declara una propiedad estatica llamada instance que permite acceder a una instancia única de la clase ScriptGameManager. 
     * El acceso a la instancia es a través de ScriptGameManager.instance.*/

    // --- Referencias a componentes ---
    [Header("Referencias a Componentes")]
    public static ScriptGameManager instance { get; private set; }
    /*Variable tipo Hud para representar el objeto Hud de nuestro juego*/
    public HUD hud;
    public Contador contador;
    public AlcoholBar02 barraAlcohol;

    // --- Acceso de lectura a estos valores ---
    /*Esto declara una propiedad publica llamada PuntosTotales que proporciona acceso a la variable privada puntosTotales. 
     * Esto permite a otras clases obtener el valor de puntos totales sin modificarlo directamente.*/
    public int PuntosTotales { get { return puntos; } }

    public float PuntosTotalAlcohol { get { return alcoholActual; } }

    // --- Variables de Alcohol / Vida ---
    [Header("Alcohol Settings")]

    /* Representa la maxima cantidad de alcohol del jugador */
    public float alcoholMax = 100;
    /* Almacena la cantidad total de vida/Alcohol del jugador */
    private float alcoholActual = 0;

    /*Esta variable privada almacena el puntaje del jugador en el juego.*/
    private int puntos;

    /* --- Variables de Fuerza --- */
    [Header("Fuerza Settings")]
    public int fuerza = 1; // Fuerza del jugador, por defecto es 1

    /* --- Eventos --- */
    public delegate void AlcoholFull(); // Evento que se dispara cuando el alcohol esta lleno
    public event AlcoholFull OnAlcoholFull; // El evento

    void Start()
    {
        barraAlcohol.InicializarBarraAlcohol(alcoholActual, alcoholMax);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            // Al presionar R, quita todo el alcohol actual con animación
            QuitarAlcohol(0.5f, 1.5f); // 0.5f: tiempo de espera, 1.5f: duración de la animación
        }*/
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            // Al presionar E, quita un poco de alcohol con animación
            QuitarAlcohol(3f, 0.5f, 1.5f); // 3f: cantidad de alcohol a quitar, 0.5f: tiempo de espera, 1.5f: duración de la animación
        }*/
    }
    /*Este metodo se llama al comienzo de la ejecucion del juego, antes del metodo Start. 
     * Se comprueba si ya existe una instancia de ScriptGameManager y, en caso contrario, se establece esta instancia como la actual. 
     * Esto garantiza que solo haya una instancia de ScriptGameManager en la escena.*/
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Mas de un GameManager en escena");
        }
        OnAlcoholFull += contador.StartCountdown; // Suscripción al evento
    }
    /*Este metodo se utiliza para agregar puntos al puntaje total del jugador. Se suma la cantidad puntosASumar a puntosTotales,
     se actualiza la interfaz de usuario a traves de hud, y se verifica si el jugador ha alcanzado ciertos puntos en escenas especificas
     para cargar escena YouWIn*/
    public void SumarPuntos(int puntosASumar)
    {
        puntos += puntosASumar;
        Debug.Log(puntos);
        hud.ActualizarPuntos(puntos);
    }

    /* Método para agregar puntos de alcohol al jugador. Se suma la cantidad de alcoholASumar a alcoholActual, se actualiza la interfaz de
       usuario desde la barra de Alcohol */
    public void ActualizarAlcohol(float alcoholASumar)
    {
        alcoholActual += alcoholASumar;
        barraAlcohol.CambiarNivelAlcohol(alcoholActual);
        if (alcoholActual >= alcoholMax)
        {
            OnAlcoholFull?.Invoke(); // Llamando al evento
        }
    }
    /* Método para agregar X cantidad de alcohol con animación. */
    public void AgregarAlcohol(float alcoholValue, float timeEspera, float timeAnimation)
    {
        StartCoroutine(barraAlcohol.ActualizarBarraDeAlcoholAnimado(alcoholValue, timeEspera, timeAnimation, () => // cuando termine la corrutina:
        {
            ActualizarAlcohol(alcoholValue);
        }));
    }

    // Método para quitar X canitdad de alcohol con animación desde la barra de alcohol
    public void QuitarAlcohol(float alcoholValue, float timeEspera, float timeAnimation)
    {
        StartCoroutine(barraAlcohol.ActualizarBarraDeAlcoholAnimado(-alcoholValue, timeEspera, timeAnimation, () => // cuando termine la corrutina:
        {
            ActualizarAlcohol(-alcoholValue);
        }));
    }

    // Método para quitar todo el alcohol con animación desde la barra de alcohol
    public void QuitarAlcohol(float timeEspera, float timeAnimation)
    {
        StartCoroutine(barraAlcohol.ActualizarBarraDeAlcoholAnimado(-alcoholActual, timeEspera, timeAnimation, () => // cuando termine la corrutina:
        {
            ActualizarAlcohol(-alcoholActual);
        }));
    }

    /*Este metodo se utiliza para restar puntos al puntaje total del jugador. Se suma la cantidad puntosASumar a puntosTotales,
    se actualiza la interfaz de usuario a traves de hud, y se verifica si el jugador ha alcanzado ciertos puntos en escenas especificas
    para cargar pantalla gameover.*/
    public void RestarPuntos(int puntosARestar)
    {
        puntos -= puntosARestar;

        Debug.Log(puntos);
        hud.ActualizarPuntos(puntos);

    }

    /* Método para actualizar la fuerza del jugador */
    public void ActualizarFuerza(int nuevaFuerza)
    {
        fuerza = nuevaFuerza;
        Debug.Log("Fuerza actualizada a: " + fuerza);
    }
}

