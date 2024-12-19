using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;
    public GameObject toDisable;

    private void Start()
    {
        StartCoroutine(FadeInEffect());
    }

    private IEnumerator FadeInEffect()
    {
        float timeElapsed = 0f;

        Color startColor = fadeImage.color;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        toDisable.SetActive(false);
    }
}
