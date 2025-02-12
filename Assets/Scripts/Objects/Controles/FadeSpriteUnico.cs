using UnityEngine;
using System.Collections;

public class FadeUnicoSprite: MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Coroutine fadeRoutine;
    public float fadeDuration = 0.5f; // Duración del fade

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0; // Inicia invisible
        spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(FadeSprite(1)); // Fade in
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(FadeSprite(0)); // Fade out
        }
    }

    private IEnumerator FadeSprite(float targetAlpha)
    {
        float startAlpha = spriteRenderer.color.a;
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}
