using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;

    public void ToMenu()
    {
        StartCoroutine(FadeAndLoadMenu());
    }
    public void ToGame()
    {
        StartCoroutine(FadeAndLoadGame());
    }
    public void QuitOnMouseDown()
    {
        Debug.Log("Exit Button Pressed");
        Application.Quit();
    }
    private IEnumerator FadeAndLoadMenu()
    {
        // Ensure the fade panel starts fully transparent
        fadePanel.alpha = 0f;

        // Fade out
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration); // Increase alpha
            yield return null;
        }

        fadePanel.alpha = 1f;

        SceneManager.LoadScene("Menu");
        // Load the new scene
        /*string sceneName = sceneToLoad != null ? sceneToLoad.name : "";
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }*/
    }
    private IEnumerator FadeAndLoadGame()
    {
        // Ensure the fade panel starts fully transparent
        fadePanel.alpha = 0f;

        // Fade out
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration); // Increase alpha
            yield return null;
        }

        fadePanel.alpha = 1f;

        SceneManager.LoadScene("Game");
        // Load the new scene
        /*string sceneName = sceneToLoad != null ? sceneToLoad.name : "";
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }*/
    }
    
}
