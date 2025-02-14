using System.Collections;
using UnityEngine;

public class Teletransportacion : MonoBehaviour
{
    public Transform targetPoint; // Punto de teletransportación objetivo
    private bool estaTeleportando = false;
    private bool jugadorEnRango = false;
    private Transform jugadorTransform; // Referencia al transform del jugador
    private FadeOut fadeOut; // Referencia al script FadeOut

    private void Start()
    {
        // Encontrar el objeto FadeOut en la escena
        fadeOut = Object.FindFirstObjectByType<FadeOut>();
        if (fadeOut == null)
        {
            Debug.LogError("No se encontró el objeto FadeOut en la escena.");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && !estaTeleportando && jugadorEnRango)
        {
            StartCoroutine(RespawnTeleport());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
            jugadorTransform = collision.transform; // Guardar la referencia al transform del jugador
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            jugadorTransform = null; // Limpiar la referencia al transform del jugador
        }
    }

    private IEnumerator RespawnTeleport()
    {
        if (fadeOut == null)
        {
            yield break; // Salir si no se encontró el objeto FadeOut
        }

        estaTeleportando = true;

        yield return StartCoroutine(fadeOut.DarkenSprite());

        if (jugadorTransform != null)
        {
            jugadorTransform.position = targetPoint.position; // Teletransportar al jugador
        }

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(fadeOut.FadeOutSprite());

        estaTeleportando = false;
    }
}





