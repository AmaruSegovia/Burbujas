using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColision : MonoBehaviour
{

    // Diccionario para manejar las colisiones por tag
    private Dictionary<string, ICollision> collisionHandlers = new Dictionary<string, ICollision>();

    private void Start()
    {
        // Registrar los manejadores de colisiones para cada tag
        collisionHandlers.Add("Vacio", new VacioCollision());
        collisionHandlers.Add("Door", new DoorCollision());
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // Verificar si existe un manejador para el tag del objeto
        if (collisionHandlers.ContainsKey(other.tag))
        {
            // Delegar el manejo de la colision al manejador correspondiente
            collisionHandlers[other.tag].HandleCollision(other);
        }
    }
}
