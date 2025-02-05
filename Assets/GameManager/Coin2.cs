using UnityEngine;


public class Coin2 : MonoBehaviour
{
    /*Esta variable representa el valor de los puntos que otorga esta moneda al ser recogida.*/
    [SerializeField] private int scoreValue = 100;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verifica si el objeto tiene el tag "Player"
        { // Muestra un mensaje en la consola indicando que la moneda fue recogida y la cantidad de puntos obtenidos.
            Debug.Log("Moneda recogida, puntos: " + scoreValue);

            ScriptGameManager.instance.SumarPuntos(scoreValue); // Suma los puntos al GameManager
            Destroy(gameObject); // Destruye la moneda
        }
    }
}
