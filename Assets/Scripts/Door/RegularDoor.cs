using UnityEngine;

public class RegularDoor : Door
{
    public int numberScene;
    public override void HandlePlayerCollision(Collider2D collider)
    {
        Debug.Log("Volver a");
        // Aquí iria la animacion de la puerta.
 
        // Cambio de escena a la anterior
        SceneLoad.Instance.LoadPreviousScene(numberScene);
    }

}
