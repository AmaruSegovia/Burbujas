using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Ladron : MonoBehaviour
{
    public float velocidad = 1.5f;
    public float velocidadCerca = 3f;
    public float distanciaDeteccion = 3f;
    public float fuerzaLanzamiento = 15f;
    private bool mirandoDerecha = true;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Transform jugador;
    private Transform ladronTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ladronTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (jugador != null)
        {
            float distancia = Vector2.Distance(transform.position, jugador.position);
            velocidad = (distancia < distanciaDeteccion) ? velocidadCerca : 1f;
        }


        rb.linearVelocity = new Vector2((mirandoDerecha ? 1 : -1) * velocidad, rb.linearVelocity.y);

        animator.SetBool("run", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstaculo"))
        {
            LanzarObstaculo(collision.gameObject);
        }
        if (collision.CompareTag("fin"))
        {
            Girar();
        }
        if (collision.CompareTag("Alcantarilla"))
        {
            StartCoroutine(HandleAlcantarillaCollision());
        }
    }

    private void LanzarObstaculo(GameObject obstaculo)
    {
        Rigidbody2D rbObstaculo = obstaculo.GetComponent<Rigidbody2D>();
        if (rbObstaculo != null)
        {
            Vector2 direccionLanzamiento = new Vector2((mirandoDerecha ? -1 : 1), 0.35f).normalized * fuerzaLanzamiento;
            rbObstaculo.linearVelocity = direccionLanzamiento;
            animator.SetTrigger("launch");
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator HandleAlcantarillaCollision()
    {
        distanciaDeteccion = 0f;
        velocidad = 0f;
        spriteRenderer.DOColor(Color.black, 1f);
        ladronTransform.DOMoveY(transform.position.y - 2f, 1f);
        yield return new WaitForSeconds(1f); // :D
    }
}






