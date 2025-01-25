using System.Collections;
using UnityEngine;

public class Sidra : Bebida
{
    private float fuerzaLanzamiento;
    private Transform transformJugador;
    private Rigidbody2D rbJugador;
    private ParticleSystem sistemaParticulas;

    public Sidra(float fuerzaLanzamiento, Transform transformJugador, Rigidbody2D rbJugador, ParticleSystem sistemaParticulas)
    {
        this.fuerzaLanzamiento = fuerzaLanzamiento;
        this.transformJugador = transformJugador;
        this.rbJugador = rbJugador;
        this.sistemaParticulas = sistemaParticulas;
    }

    public override string Nombre => "Sidra";
    public override string AnimTrigger => "BeberSidra";

    public override void Activate()
    {
        transformJugador.GetComponent<MonoBehaviour>().StartCoroutine(ApplyBoost());
    }

    public override void alcoholDrink(){
        AlcoholBar.Instance.beber(1);
    }

    private IEnumerator ApplyBoost()
    {
        yield return new WaitForSeconds(1f);
        rbJugador.linearVelocity = Vector2.zero;
        rbJugador.AddForce(Vector2.up * fuerzaLanzamiento, ForceMode2D.Impulse);
        sistemaParticulas.Play();
    }
}
