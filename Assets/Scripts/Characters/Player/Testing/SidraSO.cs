using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Sidra", menuName = "Bebidas/Nueva Sidra")]
public class SidraSO : BebidaSO
{
    public float fuerzaLanzamiento;
    public ParticleSystem sistemaParticulas;

    public override void Activate(Transform jugador)
    {
        Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
        jugador.GetComponent<MonoBehaviour>().StartCoroutine(ApplyBoost(rbJugador));
    }

    private IEnumerator ApplyBoost(Rigidbody2D rbJugador)
    {
        yield return new WaitForSeconds(1f);
        rbJugador.linearVelocity = Vector2.zero;
        rbJugador.AddForce(Vector2.up * fuerzaLanzamiento, ForceMode2D.Impulse);
        sistemaParticulas?.Play();
    }
}
