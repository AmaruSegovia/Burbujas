using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorBebidas : MonoBehaviour
{
    public static GestorBebidas Instance { get; private set; }
    private List<Bebida> bebidas = new List<Bebida>();
    private int indiceBebidaActual = 0;
    private float tiempoEspera = 3f;
    private float tiempoUltimaBebidaUsada = -Mathf.Infinity;
    private bool escudoActivo = false; 
    [Header("Bebidas UI")]
    public SpriteRenderer spriteRendererBebidas;
    public List<Sprite> bebidasIconos;

    private Transform transformJugador;
    private Rigidbody2D rigidBodyJugador;
    private Animator animatorJugador;

    [Header("Part�culas")]
    public ParticleSystem sistemaParticulasBurbujasSidra;
    public ParticleSystem particulasCerveza;

    [Header("Escudo")]
    public GameObject escudoJugador;  
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            if (escudoActivo)
            {
                Debug.Log("El jugador cayó al agua, pero fue protegido por el escudo.");
            }
            else
            {
                Debug.Log("El jugador murió al caer al agua.");
                // aqui deberia ir la logica de muerte del jugador
            }
        }
    }

    public void SetEscudoActivo(bool activo)
    {
        escudoActivo = activo;
    }

    void Start()
    {
        transformJugador = GetComponent<Transform>();
        rigidBodyJugador = GetComponent<Rigidbody2D>();
        animatorJugador = GetComponent<Animator>(); 

        bebidas.Add(new Vodka(8, 5000, transformJugador, particulasCerveza));
        bebidas.Add(new Sidra(75, transformJugador, rigidBodyJugador, sistemaParticulasBurbujasSidra));
        bebidas.Add(new Cerveza(transformJugador, escudoJugador, 4f));
        ActualizarSprite();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            BebidaSiguiente();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            BebidaAnterior();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (tiempoEsperaCompletado())
            {
                TomarBebidaActual();
            }
            else
            {
                Debug.Log($"Debes esperar {tiempoEspera - (Time.time - tiempoUltimaBebidaUsada):F2} segundos para tomar otra bebida"); // :V
            }
        }

        if (!spriteRendererBebidas.enabled && tiempoEsperaCompletado())
        {
            spriteRendererBebidas.enabled = true;
        }
    }

    void BebidaSiguiente()
    {
        indiceBebidaActual = (indiceBebidaActual + 1) % bebidas.Count;
        ActualizarSprite();
        Debug.Log($"Bebida actual: {bebidas[indiceBebidaActual].Nombre}");
    }

    void BebidaAnterior()
    {
        indiceBebidaActual = (indiceBebidaActual - 1 + bebidas.Count) % bebidas.Count;
        ActualizarSprite();
        Debug.Log($"Bebida actual: {bebidas[indiceBebidaActual].Nombre}");
    }

    void TomarBebidaActual()
    {
        bebidas[indiceBebidaActual].Activate();
        bebidas[indiceBebidaActual].alcoholDrink();        

        string animTrigger = bebidas[indiceBebidaActual].AnimTrigger;
        if (!string.IsNullOrEmpty(animTrigger) && animatorJugador != null)
        {
            animatorJugador.SetTrigger(animTrigger);
        }
        tiempoUltimaBebidaUsada = Time.time;

        StartCoroutine(DesactivarSpriteConRetraso(0.1f));
    }
    IEnumerator DesactivarSpriteConRetraso(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRendererBebidas.enabled = false;
    }

    bool tiempoEsperaCompletado()
    {
        return Time.time >= tiempoUltimaBebidaUsada + tiempoEspera;
    }

    void ActualizarSprite()
    {
        if (bebidasIconos != null && bebidasIconos.Count > indiceBebidaActual)
        {
            spriteRendererBebidas.sprite = bebidasIconos[indiceBebidaActual];
            spriteRendererBebidas.enabled = true;
        }
        else
        {
            Debug.LogWarning("Faltan sprites para las bebidas");
        }
    }
}
