using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [SerializeField] private float jumpForce = 5f; // Fuerza del salto
    [SerializeField] private LayerMask groundLayer; // Capa para detectar el suelo
    [SerializeField] private float rCircle;
    [SerializeField] private Vector2 posCircle;
    private bool isGrounded; // Para saber si estamos tocando el suelo
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Conseguimos el Rigidbody2D del jugador
    }

    void Update()
    {
        // Hacemos un colisionador circular desde la parte inferior del personaje para detectar la colision con la capa suelo
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + posCircle, rCircle, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Si estamos en el suelo y presionamos la tecla de salto
        {
            Jump();
        }
    }

    public void Jump()
    {
        // Aplicamos una fuerza hacia arriba con la fuerza del salto que elijamos en el inspector
        rb.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
    }

    // Visualiza el colisionador en Scene para depuración
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + posCircle.x, transform.position.y + posCircle.y), rCircle);
    }
}
