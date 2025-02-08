using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 60f;
    public float coyoteTime = 0.2f; // Tiempo para saltar despues de tocar piso
    private float coyoteTimer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool jumpPressed;
    private bool facingRight = true;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float move = Input.GetAxisRaw("Horizontal"); // Movimiento horizontal del personaje
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("caminando", move != 0);

        /* Condicional para voltear el sprite del personaje*/
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        /* Condicioal para detectar el slato del personaje*/
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
    }

    void FixedUpdate()
    {

        if (isGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.fixedDeltaTime;


        if (jumpPressed && coyoteTimer > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimer = 0;
            isGrounded = false;
        }

        jumpPressed = false;
    }
    /*Metodo que cambia la direccion que mira el personaje.*/
    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    /*Metodo para detectar cuando el personaje toca el suelo usando el tag Ground*/
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
            isGrounded = false;
    }
}
