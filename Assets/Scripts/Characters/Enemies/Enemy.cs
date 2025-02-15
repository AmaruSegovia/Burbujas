using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        animator.SetTrigger("muriendo");
    }

    // Este m�todo ser� llamado desde un evento de animaci�n
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}









