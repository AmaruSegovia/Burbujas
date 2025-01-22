using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float speed = 5.0f;  // Velocidad m�xima

    [SerializeField]
    private float acceleration = 10.0f;  // Qu� tan r�pido alcanza la velocidad m�xima
    [SerializeField]
    private float deceleration = 10.0f;  // Qu� tan r�pido se detiene

    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 movementInput;
    private Vector2 currentVelocity;

    void Update()
    {
        // Obtener entrada del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Almacenar la direcci�n en un vector2
        movementInput = new Vector2(horizontalInput, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        // Si hay movimiento input, aceleramos hacia la velocidad deseada
        if (movementInput != Vector2.zero)
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, movementInput * speed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Si no hay input, desaceleramos
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        // Aplicar la velocidad al Rigidbody2D
        rb.linearVelocity = currentVelocity;
    }
}