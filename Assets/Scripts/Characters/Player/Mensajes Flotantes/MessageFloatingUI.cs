using UnityEngine;
using TMPro;
using System.Collections;

public class MessageFloatingUI : MonoBehaviour
{
    public static MessageFloatingUI Instance; // Permite llamarlo desde cualquier parte del código

    [SerializeField] private TextMeshPro textMesh; // Texto flotante
    [SerializeField] private CanvasGroup canvasGroup; // Para el fade
    [SerializeField] private Transform player; // Referencia al jugador
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0); // Offset sobre el jugador
    [SerializeField] private float fadeDuration = 0.5f; // Duración del fade

    private Coroutine currentRoutine;

    void Awake()
    {
        Instance = this;
        textMesh.text = "";
        canvasGroup.alpha = 0;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
            transform.rotation = Quaternion.identity; // Evita que el texto rote con la cámara
        }
    }

    public void ShowMessage(string message, float duration = 1.5f)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        textMesh.text = message;

        // Fade In
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        // Fade Out
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }

        textMesh.text = "";
    }
}
