using UnityEngine;

public class LockedDoor : Door
{
    public int numberScene;
    public override void HandlePlayerCollision(Collider2D collider)
    {
        Debug.Log("La puerta está bloqueada. Necesitas una llave.");
        // poner si jugador tiene una llave.

        // Cambiando de escena
        SceneLoad.Instance.LoadNextScene(numberScene);
        
    }
}
