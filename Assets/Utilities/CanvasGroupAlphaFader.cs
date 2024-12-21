using UnityEngine;

public class CanvasGroupAlphaFader : MonoBehaviour
{
    public CanvasGroup canvasGroup; 
    public float fadeDuration = 1f; 

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(0f, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(1f, 0f));
    }

    private System.Collections.IEnumerator FadeCanvasGroup(float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
