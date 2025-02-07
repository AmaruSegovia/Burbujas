using UnityEngine;

public class SeguirJugadorSuelo : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaJugador;
    public Transform transformJugador;

    public EstadosMovimiento estadoActual;
    public enum EstadosMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo

    }

    public float velocidadMovimiento;
    public float distanciaMaxima;
    public Vector3 puntoInicial;
    public bool mirandoDerecha;
    public Rigidbody2D rb;
    // public Animator animator;

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


        }




    }
    public void EstadoEsperando()
    {

        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if (jugadorCollider)
        {

            transformJugador = jugadorCollider.transform;
            estadoActual = EstadosMovimiento.Siguiendo;
        }

    }

    public void EstadoSiguiendo()
    {
        //animator.SetBool("run", true);

        if (transformJugador == null)
        {

            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }

        if(transform.position.x < transformJugador.position.x)
        {
            rb.linearVelocity = new Vector2(velocidadMovimiento , rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidadMovimiento , rb.linearVelocity.y);
        }

        GiraraAObjetivo(transformJugador.position);

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
            rb.linearVelocity = new Vector2(velocidadMovimiento , rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidadMovimiento , rb.linearVelocity.y);
        }
        GiraraAObjetivo(puntoInicial);

        if (Vector2.Distance(transform.position, puntoInicial) < 1f)
        {
            rb.linearVelocity = Vector2.zero;

            //animator.SetBool("run", false);

            estadoActual = EstadosMovimiento.Esperando;

        }
    }

    private void GiraraAObjetivo(Vector3 objetivo)
    {
        if (objetivo.x < transform.position.x && !mirandoDerecha)
        {
            Girar();
        }
        else if (objetivo.x > transform.position.x && mirandoDerecha)
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
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
    }

}
