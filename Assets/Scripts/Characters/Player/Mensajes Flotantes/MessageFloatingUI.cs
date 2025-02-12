using UnityEngine;
using TMPro;
using System.Collections;

public class MessageFloatingUI : MonoBehaviour
{
    public static MessageFloatingUI Instance; // Permite llamarlo desde cualquier parte del código

    [SerializeField] private TextMeshPro textMesh; // Texto flotante
    [SerializeField] private Transform player; // Referencia al jugador
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0); // Offset sobre el jugador
    [SerializeField] private Animator animator;
    private Coroutine currentRoutine;

    void Awake()
    {
        Instance = this;
        textMesh.text = "";
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
            transform.rotation = Quaternion.identity; // Evita que el texto rote con la cámara
        }
    }

    public void ShowMessage(string message, float duration = 1.1f)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        textMesh.text = message;

        animator.SetTrigger("Fade");

        yield return new WaitForSeconds(duration);

        textMesh.text = "";
    }
}
