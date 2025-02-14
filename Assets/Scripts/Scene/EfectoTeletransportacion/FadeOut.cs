using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public float darkenDuration = 1f; 
    public float fadeDuration = 2f;   

    public IEnumerator DarkenSprite()
    {
        yield return spriteRenderer.DOColor(Color.black, darkenDuration).WaitForCompletion();
    }

    public IEnumerator FadeOutSprite()
    {
        yield return spriteRenderer.DOFade(0f, fadeDuration).WaitForCompletion();
    }
}