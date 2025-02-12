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

    /*[SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorTecho;
    [SerializeField] private float radioTecho;
    [SerializeField] private float multiplicadorVelocidadAgachado;
    [SerializeField] private Collider2D colisionadorAgachado;
    private bool estabaAgachado = false;
    private bool agachar = false;*/
    public CircleCollider2D circleCollider;
    public float StandingSize;
    public float CrouchingSize;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        circleCollider = GetComponent<CircleCollider2D>();
        //circleCollider.radius = StandingSize;

        StandingSize = circleCollider.radius; 
    }

    void Update()
    {

        float move = Input.GetAxisRaw("Horizontal"); // Movimiento horizontal del personaje
        //float move2 = Input.GetAxisRaw("Vertical"); //Movimiento vertical del personaje
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


        if (Input.GetKeyDown(KeyCode.C))
        {
            circleCollider.radius = CrouchingSize;
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            circleCollider.radius = StandingSize;
        }
        /*if(move2 < 0)
        {
            agachar = true;
        }
        else
        {
            agachar = false;
        }

        if(!agachar)
        {
            if(Physics2D.OverlapCircle(controladorTecho.position, radioTecho, queEsSuelo))
            {
                agachar = true;
            }
        }
        if(agachar){
            if(!estabaAgachado){
                estabaAgachado = true;
            }
            
            move *= multiplicadorVelocidadAgachado;
            colisionadorAgachado.enabled = false;
        }
        else
        {
            colisionadorAgachado.enabled = true;

            if(estabaAgachado)
            {
                estabaAgachado = false;
            }
        }*/
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
