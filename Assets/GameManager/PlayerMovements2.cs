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
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("caminando", move != 0);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;

        if (Input.GetKeyDown(KeyCode.C))
            circleCollider.radius = CrouchingSize;
        if (Input.GetKeyUp(KeyCode.C))
            circleCollider.radius = StandingSize;

        if (enEscondite && Input.GetKey(KeyCode.E))
        {
            ocultandose = true;
            SetTransparency(0.2f);
        }
        else
        {
            ocultandose = false;
            SetTransparency(1f);
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
        if (collision.gameObject.CompareTag("suelo"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
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
