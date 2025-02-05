using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Esta clase representa una moneda en el juego y define su comportamiento al ser recogida por el jugador.*/
public class Coin : MonoBehaviour, ICollectible
{
    /*Esta variable representa el valor de los puntos que otorga esta moneda al ser recogida.*/
    [SerializeField] private int scoreValue = 100;

    /* Este metodo implementado de la interfaz ICollectible. 
        Se ejecuta cuando el jugador recoge esta moneda.*/
    public void Collect(Player player)
    {
        // Muestra un mensaje en la consola indicando que la moneda fue recogida y la cantidad de puntos obtenidos.
        Debug.Log("Moneda recogida, puntos: " + scoreValue);

        // Encuentra el objeto GameManager en la escena y le añade los puntos correspondientes.
        ScriptGameManager.instance.SumarPuntos(scoreValue);

        Destroy(gameObject);        // Destruye el objeto moneda para eliminarlo del juego.

    }
}