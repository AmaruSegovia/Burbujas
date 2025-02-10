using UnityEngine;

public class Ladron : MonoBehaviour
{
    public float velocidad = 3f;
    public float fuerzaLanzamiento = 15f;
    private bool mirandoDerecha = true;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimiento continuo en la direccion actual
        rb.linearVelocity = new Vector2((mirandoDerecha ? 1 : -1) * velocidad, 0);

        // Activar la animación de caminar
        animator.SetBool("run", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstaculo"))
        {
            LanzarObstaculo(collision.gameObject); // Lanzar el obstaculo en la direccion opuesta
        }
        if (collision.CompareTag("fin"))
        {
            Girar(); // Cambiar la direccion del enemigo
        }
    }

    private void LanzarObstaculo(GameObject obstaculo)
    {
        //se intenta obtener el componente de fisicas Rigidbody2D del obstaculo
        Rigidbody2D rbObstaculo = obstaculo.GetComponent<Rigidbody2D>();
        //se verifica si el obstáculo tiene un Rigidbody2D
        if (rbObstaculo != null)
        {
            //mirandoaladerecha == true, si el enemigo esta mirando a la derecha el obstaculo debe ser lanzado a la izquierda 
            //mirandoaladerecha == false, si el enemigo esta mirando a la izquierda el obstaculo debe ser lanzado a la derecha (Vector2.right).
            Vector2 direccionLanzamiento = new Vector2((mirandoDerecha ? -1 : 1), 0.35f).normalized * fuerzaLanzamiento;
            //se stablece directamente la velocidad del Rigidbody2D, haciendo que el objeto se mueva en una direccion especifica con fuerza determinada
            rbObstaculo.linearVelocity = direccionLanzamiento;

            // Activar la animación de lanzar
            animator.SetTrigger("launch");
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
