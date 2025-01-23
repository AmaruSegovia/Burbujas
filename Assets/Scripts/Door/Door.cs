using UnityEngine;

public abstract class Door : MonoBehaviour
{
    public abstract void HandlePlayerCollision(Collider2D collider);

}
