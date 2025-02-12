using UnityEngine;
using System.Collections;

public class FadeSpritesHijos : MonoBehaviour
{
    private SpriteRenderer[] sprites;
    private Coroutine fadeRoutine;
    public float fadeDuration = 0.5f;

    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = sprite.color;
            color.a = 0;
            sprite.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(FadeSprites(1));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(FadeSprites(0));
        }
    }

    private IEnumerator FadeSprites(float targetAlpha)
    {
        float startAlpha = sprites[0].color.a;
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);

            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            }

            yield return null;
        }
    }
}
