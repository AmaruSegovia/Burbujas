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

    // Este método será llamado desde un evento de animación
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}









