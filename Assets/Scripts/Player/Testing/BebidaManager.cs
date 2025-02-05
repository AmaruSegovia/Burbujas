using System;
using System.Collections.Generic;
using UnityEngine;

public class BebidaManager : MonoBehaviour
{
    public static BebidaManager Instance { get; private set; }

    public event Action<BebidaSO> OnBebidaTomada;

    public List<BebidaSO> bebidas = new List<BebidaSO>();
    private int indiceBebidaActual = 0;
    private Dictionary<BebidaSO, float> tiemposUltimaBebida = new Dictionary<BebidaSO, float>(); 

    [Header("Bebidas UI")]
    public SpriteRenderer spriteRendererBebidas;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var bebida in bebidas)
        {
            tiemposUltimaBebida[bebida] = -Mathf.Infinity;
        }
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0) BebidaSiguiente();
        else if (Input.mouseScrollDelta.y < 0) BebidaAnterior();

        if (Input.GetMouseButtonDown(0) && CooldownCompletado(bebidas[indiceBebidaActual]))
        {
            TomarBebidaActual();
        }

        if (!spriteRendererBebidas.enabled && CooldownCompletado(bebidas[indiceBebidaActual]))
        {
            spriteRendererBebidas.enabled = true;
        }
    }

    private void BebidaSiguiente()
    {
        indiceBebidaActual = (indiceBebidaActual + 1) % bebidas.Count;
        ActualizarSprite();
    }

    private void BebidaAnterior()
    {
        indiceBebidaActual = (indiceBebidaActual - 1 + bebidas.Count) % bebidas.Count;
        ActualizarSprite();
    }

    private void TomarBebidaActual()
    {
        BebidaSO bebida = bebidas[indiceBebidaActual];

        bebida.Activate(transform);
        AlcoholBar.Instance.beber(bebida.AlcoholValue);
        AlcoholBar02.Instance.AgregarAlcohol(bebida.AlcoholValue, 1f, 0.4f);    //estos ultimos 2 serian el tiempo de espera antes de la animacion y el tiempo de la animacion; esto podria variar segun la bebida
        OnBebidaTomada?.Invoke(bebida);

        tiemposUltimaBebida[bebida] = Time.time;

        if (bebida.sonido != null && audioSource != null)
        {
            audioSource.PlayOneShot(bebida.sonido);
        }

        StartCoroutine(DesactivarSpriteConRetraso(0.1f));
    }

    private void ActualizarSprite()
    {
        spriteRendererBebidas.sprite = bebidas[indiceBebidaActual].Icono;
    }

    private bool CooldownCompletado(BebidaSO bebida)
    {
        return Time.time >= tiemposUltimaBebida[bebida] + bebida.cooldown;
    }

    private System.Collections.IEnumerator DesactivarSpriteConRetraso(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRendererBebidas.enabled = false;
    }
}
