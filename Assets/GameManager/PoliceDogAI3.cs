using System.Collections;
using UnityEngine;

public class PoliceDogAI3 : MonoBehaviour
{
    public float radioBusqueda;
    public float anguloVision = 45f;
    public LayerMask capaJugador;
    public LayerMask capaHueso;
    public Transform transformJugador;
    private Transform huesoObjetivo;
    public EstadosMovimiento estadoActual;

    public enum EstadosMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo,
        BuscandoHueso,
        ComiendoHueso
    }

    public float velocidadMovimiento;
    public float velocidadPatrullaje;

    public float distanciaMaxima;
    public Vector3 puntoInicial;
    public bool mirandoDerecha;
    private Rigidbody2D rb;
    private Coroutine patrullajeCoroutine;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        puntoInicial = transform.position;
        estadoActual = EstadosMovimiento.Esperando;

        // Iniciar mirando en la direccion opuesta
        mirandoDerecha = !mirandoDerecha;
        Girar(); // Aplicar el cambio visual y logico

        patrullajeCoroutine = StartCoroutine(PatrullarEsperando());
    }

    private void Update()
    {
        switch (estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;

            case EstadosMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;

            case EstadosMovimiento.Volviendo:
                EstadoVolviendo();
                break;

            case EstadosMovimiento.BuscandoHueso:
                EstadoBuscandoHueso();
                break;

            case EstadosMovimiento.ComiendoHueso:
                // Controlado por la corrutina :D
                break;
        }
    }

    private void EstadoEsperando()
    {
        animator.SetBool("caminata", true);

        if (DetectarJugador())
        {
            if (patrullajeCoroutine != null)
            {
                StopCoroutine(patrullajeCoroutine);
                patrullajeCoroutine = null;
            }
            estadoActual = EstadosMovimiento.Siguiendo;
            return;
        }

        if (estadoActual == EstadosMovimiento.Siguiendo)
            return;

        huesoObjetivo = BuscarHuesoMasCercano();
        if (huesoObjetivo != null)
        {
            if (patrullajeCoroutine != null)
            {
                StopCoroutine(patrullajeCoroutine);
                patrullajeCoroutine = null;
            }
            estadoActual = EstadosMovimiento.BuscandoHueso;
        }
    }

    private bool DetectarJugador()
    {
        Collider2D[] jugadores = Physics2D.OverlapCircleAll(transform.position, radioBusqueda, capaJugador);

        foreach (var jugador in jugadores)
        {
            // Obtener el script del jugador
            PlayerMovements2 playerScript = jugador.GetComponent<PlayerMovements2>();

            // Si el jugador esta oculto,se lo ignora
            if (playerScript != null && playerScript.EstaOculto())
            {
                continue;
            }

            Vector3 direccionAlJugador = (jugador.transform.position - transform.position).normalized;
            float angulo = Vector3.Angle(transform.right * (mirandoDerecha ? 1 : -1), direccionAlJugador);

            if (angulo < anguloVision)
            {
                transformJugador = jugador.transform;
                animator.SetTrigger("atacar");
                return true;
            }
        }
        return false;
    }

    private void EstadoSiguiendo()
    {
        animator.SetBool("caminata", true);

        if (transformJugador == null)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }

        MoverHacia(transformJugador.position);

        if (Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima ||
            Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            transformJugador = null;
        }
    }

    private void EstadoVolviendo()
    {
        animator.SetBool("caminata", true);

        MoverHacia(puntoInicial);

        if (Vector2.Distance(transform.position, puntoInicial) < 2f)
        {
            rb.linearVelocity = Vector2.zero;
            estadoActual = EstadosMovimiento.Esperando;
            patrullajeCoroutine = StartCoroutine(PatrullarEsperando());
        }
    }

    private void EstadoBuscandoHueso()
    {
        animator.SetBool("caminata", true);

        if (huesoObjetivo == null)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }

        MoverHacia(huesoObjetivo.position);

        if (Vector2.Distance(transform.position, huesoObjetivo.position) < 0.5f)
        {
            estadoActual = EstadosMovimiento.ComiendoHueso;
            StartCoroutine(ComerHueso());
        }
    }

    private IEnumerator ComerHueso()
    {
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(5f);
        estadoActual = EstadosMovimiento.Volviendo;
    }

    private Transform BuscarHuesoMasCercano()
    {
        Collider2D[] huesos = Physics2D.OverlapCircleAll(transform.position, distanciaMaxima, capaHueso);
        Transform huesoCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (Collider2D hueso in huesos)
        {
            float distancia = Vector2.Distance(transform.position, hueso.transform.position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                huesoCercano = hueso.transform;
            }
        }
        return huesoCercano;
    }

    private void MoverHacia(Vector3 objetivo)
    {
        float direccion = (objetivo.x > transform.position.x) ? 1 : -1;
        rb.linearVelocity = new Vector2(velocidadMovimiento * direccion, 0);
        GiraraAObjetivo(objetivo);
    }

    private void GiraraAObjetivo(Vector3 objetivo)
    {
        if ((objetivo.x > transform.position.x && !mirandoDerecha) ||
            (objetivo.x < transform.position.x && mirandoDerecha))
        {
            Girar();
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha; // Invertir direccion

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // Invertir la escala para reflejar la nueva direccion
    }

    /*private IEnumerator PatrullarEsperando()
    {
        while (estadoActual == EstadosMovimiento.Esperando)
        {
            yield return new WaitForSeconds(2f);

            Girar();

            float direccionMovimiento = mirandoDerecha ? 1 : -1;

            float velocidadObjetivo = velocidadPatrullaje * direccionMovimiento;
            float velocidadActual = rb.linearVelocity.x;

            float tiempoDeAceleracion = 0.5f;
            float tiempoInicio = Time.time;

            while (Time.time < tiempoInicio + tiempoDeAceleracion)
            {
                float progreso = (Time.time - tiempoInicio) / tiempoDeAceleracion;
                rb.linearVelocity = new Vector2(Mathf.Lerp(velocidadActual, velocidadObjetivo, progreso), rb.linearVelocity.y);
                yield return null; // Esperar un frame antes de continuar
            }

            rb.linearVelocity = new Vector2(velocidadObjetivo, rb.linearVelocity.y);
        }
    }*/

    private IEnumerator PatrullarEsperando()
    {
        while (estadoActual == EstadosMovimiento.Esperando)
        {
            float direccionMovimiento = mirandoDerecha ? 1 : -1;
            float velocidadObjetivo = velocidadPatrullaje * direccionMovimiento;
            float velocidadInicial = rb.linearVelocity.x;
            float tiempoInicio = Time.time;
            float tiempoDuracion = 3f;

            while (Time.time < tiempoInicio + tiempoDuracion)
            {
                float progreso = (Time.time - tiempoInicio) / tiempoDuracion;
                rb.linearVelocity = new Vector2(Mathf.Lerp(velocidadInicial, velocidadObjetivo, progreso), rb.linearVelocity.y);
                yield return null;
            }

            rb.linearVelocity = new Vector2(velocidadObjetivo, rb.linearVelocity.y);

            yield return new WaitForSeconds(0.5f);

            Girar();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciaMaxima);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);

        // Campo de vision en forma de triangulo amarillo
        Vector3 origen = transform.position;
        Vector3 direccionDerecha = Quaternion.Euler(0, 0, anguloVision) * (transform.right * (mirandoDerecha ? 1 : -1)) * radioBusqueda;
        Vector3 direccionIzquierda = Quaternion.Euler(0, 0, -anguloVision) * (transform.right * (mirandoDerecha ? 1 : -1)) * radioBusqueda;

        Gizmos.DrawLine(origen, origen + direccionDerecha);
        Gizmos.DrawLine(origen, origen + direccionIzquierda);
        Gizmos.DrawLine(origen + direccionDerecha, origen + direccionIzquierda);
    }
}


