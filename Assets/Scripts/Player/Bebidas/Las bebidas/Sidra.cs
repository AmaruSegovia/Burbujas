using System.Collections;
using UnityEngine;

public class Sidra : Bebida
{
    private float fuerzaLanzamiento;
    private Transform transformJugador;
    private Rigidbody2D rbJugador;

    public Sidra(float fuerzaLanzamiento, Transform transformJugador, Rigidbody2D rbJugador)
    {
        this.fuerzaLanzamiento = fuerzaLanzamiento;
        this.transformJugador = transformJugador;
        this.rbJugador = rbJugador;
    }

    public override string Nombre => "Sidra";

    public override void Activate()
    {
        transformJugador.GetComponent<MonoBehaviour>().StartCoroutine(ApplyBoost());
    }

    private IEnumerator ApplyBoost()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicionMouse.z = 0;
        Vector2 direction = (posicionMouse - transformJugador.position).normalized;
        rbJugador.linearVelocity = Vector2.zero;
        rbJugador.AddForce(direction * fuerzaLanzamiento, ForceMode2D.Impulse);
    }
}