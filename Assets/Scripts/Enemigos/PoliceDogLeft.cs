using System.Collections;
using UnityEngine;

public class ComportamientoPerroCambioDireccion : MonoBehaviour
{
    public float velocidadPatrulla = 2f;       // Velocidad del patrullaje
    public float fuerzaSalto = 6f;            // Fuerza hacia arriba para el ataque
    public float rangoVision = 5f;            // Rango de visión del perro
    public float tiempoEntreAtaques = 2f;     // Tiempo entre ataques

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool moviendoIzquierda = true;    // Dirección inicial: hacia la izquierda
    private bool persiguiendo = false;        // ¿Está persiguiendo al jugador?
    private bool puedeAtacar = true;          // ¿Puede atacar?

    public LayerMask capaJugador;             // Capa del jugador
    public Transform puntoVision;             // Punto desde donde el perro "ve"
    public BoxCollider2D boxCollider;         // Collider del perro

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Configuración inicial: moviendo hacia la izquierda
        if (!moviendoIzquierda)
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
        // Movimiento constante en la dirección actual
        float direccion = moviendoIzquierda ? -1 : 1;
        rb.linearVelocity = new Vector2(direccion * velocidadPatrulla, rb.linearVelocity.y);
    }

    private void DetectarJugador()
    {
        // Detectar al jugador dentro del rango de visión
        Collider2D[] hits = Physics2D.OverlapCircleAll(puntoVision.position, rangoVision, capaJugador);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Vector3 direccionAlJugador = (hit.transform.position - puntoVision.position).normalized;
                float angulo = Vector3.Angle((moviendoIzquierda ? -transform.right : transform.right), direccionAlJugador);

                // Verificar si el jugador está dentro del cono de visión (ángulo de 45 grados)
                if (angulo < 45f)
                {
                    persiguiendo = true;
                    break;
                }
            }
        }
    }

    private void PerseguirJugador()
    {
        // Lógica para perseguir al jugador
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador == null) return;

        float direccion = jugador.transform.position.x < transform.position.x ? -1 : 1;

        // Voltear si es necesario
        if ((direccion == -1 && !moviendoIzquierda) || (direccion == 1 && moviendoIzquierda))
        {
            Voltear();
        }

        // Movimiento hacia el jugador
        rb.linearVelocity = new Vector2(direccion * velocidadPatrulla, rb.linearVelocity.y);

        if (puedeAtacar)
        {
            StartCoroutine(SaltoAtaque(direccion));
        }
    }

    private IEnumerator SaltoAtaque(float direccion)
    {
        puedeAtacar = false;

        // Aplicar fuerza de salto hacia el jugador
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(direccion * velocidadPatrulla, fuerzaSalto), ForceMode2D.Impulse);

        // Esperar antes de permitir otro ataque
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }

    private void Voltear()
    {
        // Cambiar la dirección actual
        moviendoIzquierda = !moviendoIzquierda;

        // Voltear el sprite
        spriteRenderer.flipX = !spriteRenderer.flipX;

        // Cambiar la posición del punto de visión para que esté frente al personaje
        Vector3 nuevaPosicionPuntoVision = puntoVision.localPosition;
        nuevaPosicionPuntoVision.x *= -1; // Invertir la posición en X
        puntoVision.localPosition = nuevaPosicionPuntoVision;

        // Ajustar el offset del collider si es necesario
        Vector2 offsetCollider = boxCollider.offset;
        offsetCollider.x *= -1;
        boxCollider.offset = offsetCollider;
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        // Cambiar de dirección al chocar con una pared
        if (colision.collider.CompareTag("Wall"))
        {
            Voltear();
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar el rango de visión en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoVision.position, rangoVision);

        // Dibujar líneas para el cono de visión
        Vector3 direccion1 = Quaternion.Euler(0, 0, 45) * (moviendoIzquierda ? -transform.right : transform.right) * rangoVision;
        Vector3 direccion2 = Quaternion.Euler(0, 0, -45) * (moviendoIzquierda ? -transform.right : transform.right) * rangoVision;

        Gizmos.DrawLine(puntoVision.position, puntoVision.position + direccion1);
        Gizmos.DrawLine(puntoVision.position, puntoVision.position + direccion2);
    }
}
