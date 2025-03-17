using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionOverlay : MonoBehaviour
{
    public Image overlayImage;
    public float fadeDuration = 0.5f;

    void Awake()
    {
        // Initially fully transparent (alpha = 0)
        overlayImage.color = new Color(
            overlayImage.color.r,
            overlayImage.color.g,
            overlayImage.color.b,
            0f);
        
        overlayImage.raycastTarget = false; // so it doesn't block UI interactions
    }

    public void FadeTransition(System.Action onFadeMiddle)
    {
        StartCoroutine(FadeRoutine(onFadeMiddle));
    }

    IEnumerator FadeRoutine(System.Action onFadeMiddle)
    {
        // Fade in overlay (alpha 0 to 1)
        yield return Fade(0f, 1f);

        // Execute midpoint action (switch screens here)
        onFadeMiddle?.Invoke();

        // Fade out overlay (alpha 1 to 0)
        yield return Fade(1f, 0f);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = overlayImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            overlayImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
        overlayImage.color = new Color(c.r, c.g, c.b, endAlpha);
    }
}
