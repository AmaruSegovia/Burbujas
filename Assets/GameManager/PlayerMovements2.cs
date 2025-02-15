using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 60f;
    public float coyoteTime = 0.2f;
    private float coyoteTimer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool jumpPressed;
    private bool facingRight = true;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public CircleCollider2D circleCollider;
    public float StandingSize;
    public float CrouchingSize;

    private bool enEscondite = false;
    private bool ocultandose = false;
    private bool estaAgachado = false;  // Nueva variable para controlar el estado de agachado

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        circleCollider = GetComponent<CircleCollider2D>();
        StandingSize = circleCollider.radius;
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        // Si está agachado, reduce la velocidad
        if (estaAgachado)
            rb.linearVelocity = new Vector2(move * (moveSpeed * 0.5f), rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Animación de caminar
        animator.SetBool("caminando", move != 0 && !estaAgachado);
        animator.SetBool("caminandoAgachado", move != 0 && estaAgachado);

        // Flip del personaje
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        // Salto
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;

        // Agacharse
        if (Input.GetKeyDown(KeyCode.C))
        {
            estaAgachado = true;
            circleCollider.radius = CrouchingSize;
            animator.SetBool("agacharse", true);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            estaAgachado = false;
            circleCollider.radius = StandingSize;
            animator.SetBool("agacharse", false);
            animator.SetBool("caminandoAgachado", false);
        }

        // Ocultarse en escondite
        if (enEscondite && Input.GetKey(KeyCode.E))
        {
            ocultandose = true;
            SetTransparency(0.2f);
            gameObject.layer = LayerMask.NameToLayer("JugadorOculto");
        }
        else
        {
            ocultandose = false;
            SetTransparency(1f);
            gameObject.layer = LayerMask.NameToLayer("Jugador");
        }
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

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo") || collision.gameObject.layer == LayerMask.NameToLayer("suelo"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo") || collision.gameObject.layer == LayerMask.NameToLayer("suelo"))
            isGrounded = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Escondite"))
        {
            enEscondite = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Escondite"))
        {
            enEscondite = false;
            SetTransparency(1f);
        }
    }

    void SetTransparency(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public bool EstaOculto()
    {
        return ocultandose;
    }
}










