using UnityEngine;
using UnityEngine.SceneManagement;

public class VacioCollision : ICollision
{
    public void HandleCollision (Collider2D other)
    {
        Debug.Log("Colision con el vacio");
        SceneManager.LoadScene ("GameOver");
    }
}
