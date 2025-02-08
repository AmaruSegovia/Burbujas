using UnityEngine;
using UnityEngine.SceneManagement;

public class MuerteJugador : MonoBehaviour
{
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua") )
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.collider.CompareTag("Enemigo"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
