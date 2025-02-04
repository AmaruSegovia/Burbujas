using UnityEngine;

public class JugadorAnimaciones : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        BebidaManager.Instance.OnBebidaTomada += ActivarAnimacion;
    }

    private void OnDestroy()
    {
        if (BebidaManager.Instance != null)
        {
            BebidaManager.Instance.OnBebidaTomada -= ActivarAnimacion;
        }
    }

    private void ActivarAnimacion(BebidaSO bebida)
    {
        if (animator != null && !string.IsNullOrEmpty(bebida.AnimTrigger))
        {
            animator.SetTrigger(bebida.AnimTrigger);
        }
    }
}
