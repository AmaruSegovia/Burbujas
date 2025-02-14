using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public static FadeOut Instance { get; private set; } // Singleton instance

    public Image fadeImage; // Image de la UI para el fadeout

    public float darkenDuration = 1f;
    public float fadeDuration = 2f;

    public IEnumerator DarkenSprite()
    {
        yield return fadeImage.DOColor(Color.black, darkenDuration).WaitForCompletion();
    }

    public IEnumerator FadeOutSprite()
    {
        yield return fadeImage.DOFade(0f, fadeDuration).WaitForCompletion();
    }
}





