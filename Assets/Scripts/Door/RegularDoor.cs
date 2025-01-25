using UnityEngine;

public class RegularDoor : Door
{
    public override void HandlePlayerCollision(Collider2D collider)
    {
        Debug.Log("La puerta normal se abre.para atras");
        // Aquí iria la animacion de la puerta.

        // Cambio de escena a la anterior
        SceneLoad.Instance.LoadPreviousScene();
    }

}
