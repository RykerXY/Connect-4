using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    public SceneAsset sceneToLoad;
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;

    public void StartOnMouseDown()
    {
        StartCoroutine(FadeAndLoadScene());
    }
    public void QuitOnMouseDown()
    {
        Debug.Log("Exit Button Pressed");
        Application.Quit();
    }
    private IEnumerator FadeAndLoadScene()
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

        // Load the new scene
        string sceneName = sceneToLoad != null ? sceneToLoad.name : "";
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    
}
