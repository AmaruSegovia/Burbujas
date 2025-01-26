using UnityEngine;
using UnityEngine.SceneManagement;

public class RegularDoor : Door
{
    // Este es el transform del Empty donde quieres que el jugador se teletransporte
    public Transform spawnPoint;

    // Nombre de la escena que se debe cargar
    public string sceneName;

    public override void HandlePlayerCollision(Collider2D collider)
    {
        Debug.Log("La puerta está bloqueada. Necesitas una llave.");
        
        // Aquí puedes verificar si el jugador tiene la llave, por ejemplo:
        // if (jugador.tieneLlave) { // habilitar el paso }
        
            // Mover al jugador a la nueva posición especificada
            PlayerMovement playerController = collider.GetComponent<PlayerMovement>();
            if (playerController != null && spawnPoint != null)
            {
                playerController.transform.position = spawnPoint.position;
            }
            // Cargar la siguiente escena
            //SceneManager.LoadScene(sceneName);
        
    }

}
