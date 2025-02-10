using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDynamic = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Inicialmente kinematic
    }

    public void ApplyForce(Vector2 force)
    {
        if (!isDynamic)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Cambiar a dynamic
            isDynamic = true;
        }
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
