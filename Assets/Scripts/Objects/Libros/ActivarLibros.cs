using UnityEngine;

public class ActivarLibros : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivarTodosLosHijos(true);
        }
    }

    private void ActivarTodosLosHijos(bool estado)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(estado);
        }
    }
}