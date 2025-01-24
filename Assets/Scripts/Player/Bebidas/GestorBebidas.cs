using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorBebidas : MonoBehaviour
{
    private List<Bebida> bebidas = new List<Bebida>();
    private int indiceBebidaActual = 0;
    private float tiempoEspera = 3f;
    private float tiempoUltimaBebidaUsada = -Mathf.Infinity;

    [Header("Bebidas UI")]
    public SpriteRenderer spriteRendererBebidas;
    public List<Sprite> bebidasIconos;

    private Transform transformJugador;
    private Rigidbody2D rigidBodyJugador;
    private Animator animatorJugador;

    [Header("Partículas")]
    public ParticleSystem sistemaParticulasBurbujas;
    void Start()
    {
        transformJugador = GetComponent<Transform>();
        rigidBodyJugador = GetComponent<Rigidbody2D>();
        animatorJugador = GetComponent<Animator>(); 

        bebidas.Add(new Vodka(10, 2000, transformJugador));
        bebidas.Add(new Sidra(75, transformJugador, rigidBodyJugador, sistemaParticulasBurbujas));
        bebidas.Add(new Cerveza());
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
                Debug.Log($"Debes esperar {tiempoEspera - (Time.time - tiempoUltimaBebidaUsada):F2} segundos para tomar otra bebida");
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
