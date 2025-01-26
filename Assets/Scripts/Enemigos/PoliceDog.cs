using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamientoPerroPolicia : MonoBehaviour
{
    public float velocidadPatrulla = 2f;       // Velocidad del patrullaje
    public float velocidadPersecucion = 3.5f;  // Velocidad cuando persigue al jugador
    public float fuerzaSalto = 6f;             // Fuerza hacia arriba para el ataque
    public float rangoVision = 5f;             // Rango de visión del perro
    public float tiempoEntreAtaques = 2f;      // Tiempo entre ataques

    public bool empezarMoviendoDerecha = true; // Dirección inicial (derecha o izquierda)

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool moviendoDerecha;              // Dirección actual del perro
    private bool persiguiendo = false;         // ¿Está persiguiendo al jugador?
    private bool puedeAtacar = true;           // ¿Puede atacar?

    public LayerMask capaJugador;              // Capa del jugador
    public Transform puntoVision;              // Punto desde donde el perro "ve"
    public BoxCollider2D boxCollider;          // Collider del perro

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializar dirección con base en el valor de empezarMoviendoDerecha
        moviendoDerecha = empezarMoviendoDerecha;

        // Configuración inicial del SpriteRenderer y BoxCollider
        if (!moviendoDerecha)
        {
            Voltear();
        }
    }

    void Update()
    {
        if (!persiguiendo)
        {
            Patrullar();
        }
        else
        {
            PerseguirJugador();
        }

        DetectarJugador();
    }

    private void Patrullar()
    {
        float direccion = moviendoDerecha ? 1 : -1; // Determina la dirección del movimiento
        rb.linearVelocity = new Vector2(direccion * velocidadPatrulla, rb.linearVelocity.y);
    }

    private void DetectarJugador()
    {
        // Detección de jugador con un OverlapCircleAll
        Collider2D[] hits = Physics2D.OverlapCircleAll(puntoVision.position, rangoVision, capaJugador);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Vector3 direccionAlJugador = (hit.transform.position - puntoVision.position).normalized;
                float angulo = Vector3.Angle(transform.right * (moviendoDerecha ? 1 : -1), direccionAlJugador);

                // Verificar si el jugador está dentro del cono de visión
                if (angulo < 45f) // Ángulo de 45 grados para el cono de visión
                {
                    persiguiendo = true;
                    break;
                }
            }
        }
    }

    private void PerseguirJugador()
    {
        // Obtener dirección hacia el jugador
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador == null) return;

        float direccion = jugador.transform.position.x > transform.position.x ? 1 : -1;

        // Voltear hacia el jugador
        if ((direccion > 0 && !moviendoDerecha) || (direccion < 0 && moviendoDerecha))
        {
            Voltear();
        }

        // Movimiento horizontal hacia el jugador
        rb.linearVelocity = new Vector2(direccion * velocidadPersecucion, rb.linearVelocity.y);

        if (puedeAtacar)
        {
            // Realizar el salto de ataque
            StartCoroutine(SaltoAtaque(direccion));
        }
    }

    private IEnumerator SaltoAtaque(float direccion)
    {
        puedeAtacar = false;

        // Aplicar fuerza de salto hacia el jugador
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(direccion * velocidadPersecucion, fuerzaSalto), ForceMode2D.Impulse);

        // Esperar antes de permitir otro ataque
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }

    private void Voltear()
    {
        moviendoDerecha = !moviendoDerecha; // Cambiar dirección
        spriteRenderer.flipX = !spriteRenderer.flipX; // Voltear el sprite

        // Voltear el box collider (opcional, si es necesario)
        Vector2 offsetCollider = boxCollider.offset;
        offsetCollider.x *= -1;
        boxCollider.offset = offsetCollider;
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.collider.CompareTag("Wall"))
        {
            // Cambia la dirección al chocar con una pared
            Voltear();
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar el campo de visión en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoVision.position, rangoVision);

        Vector3 direccionDerecha = Quaternion.Euler(0, 0, 45) * (transform.right * (moviendoDerecha ? 1 : -1)) * rangoVision;
        Vector3 direccionIzquierda = Quaternion.Euler(0, 0, -45) * (transform.right * (moviendoDerecha ? 1 : -1)) * rangoVision;

        Gizmos.DrawLine(puntoVision.position, puntoVision.position + direccionDerecha);
        Gizmos.DrawLine(puntoVision.position, puntoVision.position + direccionIzquierda);
    }
}
