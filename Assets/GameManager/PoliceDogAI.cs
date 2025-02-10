using System.Collections;
using UnityEngine;

public class PoliceDogAI : MonoBehaviour
{
    public float radioBusqueda;
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
    public float distanciaMaxima;
    public Vector3 puntoInicial;
    public bool mirandoDerecha;
    public Rigidbody2D rb;

    private void Start()
    {
        puntoInicial = transform.position;
    }

    public void Update()
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
                // no le puse nada solo se controla por por la corrutina :D
                break;
        }
    }

    public void EstadoEsperando()
    {
        rb.linearVelocity = Vector2.zero;
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if (jugadorCollider)
        {

            transformJugador = jugadorCollider.transform;
            estadoActual = EstadosMovimiento.Siguiendo;
            return;
        }

        /*Buscar el hueso mas cercano*/
        huesoObjetivo = BuscarHuesoMasCercano();
        if (huesoObjetivo != null)
        {
            estadoActual = EstadosMovimiento.BuscandoHueso;
        }
    }

    public void EstadoSiguiendo()
    {
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
        if (transform.position.x < puntoInicial.x)
        {
            rb.linearVelocity = new Vector2(velocidadMovimiento, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidadMovimiento, rb.linearVelocity.y);
        }
        GiraraAObjetivo(puntoInicial);

        if (Vector2.Distance(transform.position, puntoInicial) < 2f)
        {
            rb.linearVelocity = Vector2.zero;

            //animator.SetBool("run", false);

            estadoActual = EstadosMovimiento.Esperando;

        }
    }

    private void EstadoBuscandoHueso()
    {
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
        yield return new WaitForSeconds(5f); // Esperar 5 segundos
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
        rb.linearVelocity = new Vector2(velocidadMovimiento * direccion, rb.linearVelocity.y);
        GiraraAObjetivo(objetivo);
    }

    private void GiraraAObjetivo(Vector3 objetivo)
    {
        if ((objetivo.x < transform.position.x && !mirandoDerecha) ||
            (objetivo.x > transform.position.x && mirandoDerecha))
        {
            Girar();
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciaMaxima);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
    }
}

/* Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);*/