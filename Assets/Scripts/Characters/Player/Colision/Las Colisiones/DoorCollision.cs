using UnityEngine;

public class DoorCollision: ICollision
{
    public void HandleCollision (Collider2D other)
    {
        Debug.Log("Preciona E");
        Door door = other.GetComponent<Door>();
        
        if (door != null && Input.GetKeyDown(KeyCode.E))
        {
            door.HandlePlayerCollision(other);
        }
        else Debug.Log("Scene load Manager desactivado");
    }
}
