using UnityEngine;

public class DoorCollision: ICollision
{
    private bool hasCollided = false; // para que no colicione varias veces
    public void HandleCollision (Collider2D other)
    {
        Debug.Log("Colision con una puerta");
        Door door = other.GetComponent<Door>();
        
        if (door != null)
        {
            // Si es la primera vez que colisionamos con una puerta, pausamos el juego
            if (!hasCollided)
            {
                // Llamamos al método específico de la puerta
                door.HandlePlayerCollision(other);
                //Time.timeScale = 0f;  // Pausar el juego
                hasCollided = true;  // Evitar que se pause varias veces
                Debug.Log("Juego pausado por colisión con la puerta");
            }
        }
    }
}
