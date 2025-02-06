using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Cerveza", menuName = "Bebidas/Nueva Cerveza")]
public class CervezaSO : BebidaSO
{
    public float radioExplosion;
    public float fuerzaExplosion;
    public GameObject prefabParticulasExplosion;

    public override void Activate(Transform jugador)
    {
        jugador.GetComponent<MonoBehaviour>().StartCoroutine(DelayExplosion(jugador));
    }

    private IEnumerator DelayExplosion(Transform jugador)
    {
        yield return new WaitForSeconds(1f);
        Explosion(jugador);
    }

    private void Explosion(Transform jugador)
    {
        Vector3 posicionParticulas = jugador.position + new Vector3(0, -0.5f, 0);
        GameObject particulasInstanciadas = Instantiate(prefabParticulasExplosion, posicionParticulas, Quaternion.identity);

        ParticleSystem sistemaParticulas = particulasInstanciadas.GetComponent<ParticleSystem>();
        if (sistemaParticulas != null)
        {
            sistemaParticulas.Play();
        }
        Collider2D[] objetos = Physics2D.OverlapCircleAll(jugador.position, radioExplosion);
        foreach (Collider2D colisionador in objetos)
        {
            Rigidbody2D rb2D = colisionador.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direccion = colisionador.transform.position - jugador.position;
                float distancia = 1 + direccion.magnitude;
                float fuerzaFinal = fuerzaExplosion / distancia;
                rb2D.AddForce(direccion * fuerzaFinal);
            }
        }
        Destroy(particulasInstanciadas, 10f);
    }
}
