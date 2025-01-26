using UnityEngine;

public class DoorCollision: ICollision
{
    public void HandleCollision (Collider2D other)
    {
        Debug.Log("Colision con una puerta");
        Door door = other.GetComponent<Door>();
        
        if (door != null && Input.GetKeyDown(KeyCode.E))
        {
            door.HandlePlayerCollision(other);
        }
    }
}
