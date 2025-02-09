using UnityEngine;

public class Enemy2 : MonoBehaviour {
    /*Esta variable representa el valor de los puntos de vida que se le resta al jugador.*/
    [SerializeField] private int scoreValue = 10;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verifica si el objeto tiene el tag "Player"
        { // Muestra un mensaje en la consola indicando que la moneda fue recogida y la cantidad de puntos obtenidos.
            Debug.Log("Moneda recogida, puntos: " + scoreValue);

            ScriptGameManager.instance.AgregarAlcohol(scoreValue, 0.2f, 0.8f); // Resta los puntos de vida al GameManager
            //Destroy(gameObject); // Destruye la moneda
        }
    }

}
