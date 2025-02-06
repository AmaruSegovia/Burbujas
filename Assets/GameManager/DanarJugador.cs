using UnityEngine;

public class DanarJugador : MonoBehaviour, IDamageHandler
{
    /*Esta variable representa el valor de los puntos de daño.*/
    [SerializeField] private int scoreDamage = 10;

    /* Este metodo implementado de la interfaz TakeDamage. 
        Se ejecuta cuando el jugador interactua con un trampa u enemigo.*/
    public void TakeDamage(Player player)
    {
        // Encuentra el objeto GameManager en la escena y le resta los puntos correspondientes.
        ScriptGameManager.instance.RestarPuntosV(scoreDamage);

        Destroy(gameObject);        // Destruye el objeto moneda para eliminarlo del juego.

    }
}