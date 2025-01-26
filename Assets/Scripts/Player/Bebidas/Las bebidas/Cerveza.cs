using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Cerveza : Bebida
{
    public override string Nombre => "Cerveza";
    public override string AnimTrigger => "BeberCerveza";
    private Transform jugador;            // Transform del jugador
    private GameObject escudo;           // Escudo (objeto hijo del jugador)
    private float duracionEscudo = 4f;   // Duración del escudo en segundos
    private float esperaAntesDeActivar = 1f; // Tiempo antes de activar el escudo

    public Cerveza(Transform jugador, GameObject escudo, float duracionEscudo)
    {
        this.jugador = jugador;
        this.escudo = escudo;
        this.duracionEscudo = duracionEscudo;
        this.escudo.SetActive(false);
    }

    public override void Activate()
    {
        jugador.GetComponent<MonoBehaviour>().StartCoroutine(ActivarEscudoConRetraso());
    }

    private IEnumerator ActivarEscudoConRetraso()
    {
        yield return new WaitForSeconds(esperaAntesDeActivar);

        escudo.SetActive(true);
        GestorBebidas.Instance.SetEscudoActivo(true); 

        yield return new WaitForSeconds(duracionEscudo);

        float tiempoRestante = duracionEscudo;
        while (tiempoRestante > 0)
        {
            escudo.transform.position = jugador.position; // Sincronizar la posición del escudo con el jugador
            tiempoRestante -= Time.deltaTime;             // Reducir el tiempo restante
            yield return null;                            // Esperar un frame
        }
        escudo.SetActive(false);
        GestorBebidas.Instance.SetEscudoActivo(false);
    }
    public override void alcoholDrink(){
        AlcoholBar.Instance.beber(1);
    }
}