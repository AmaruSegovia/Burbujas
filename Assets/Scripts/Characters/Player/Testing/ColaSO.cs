using UnityEngine;

[CreateAssetMenu(fileName = "NuevaBebidaCola", menuName = "Bebidas/Cola")]
public class Cola : BebidaSO
{
    public override void Activate(Transform jugador)
    {
        // Aumentar la fuerza del jugador a 2
        ScriptGameManager.instance.ActualizarFuerza(2);
        // Aumentar ligeramente la barra de alcohol
        ScriptGameManager.instance.ActualizarAlcohol(AlcoholValue);

        // Reproducir el sonido de la bebida si está disponible
        if (sonido != null)
        {
            AudioSource.PlayClipAtPoint(sonido, jugador.position);
        }

        // Activar la animación del jugador si está disponible
        Animator animator = jugador.GetComponent<Animator>();
        if (animator != null && !string.IsNullOrEmpty(AnimTrigger))
        {
            animator.SetTrigger(AnimTrigger);
        }
    }
}