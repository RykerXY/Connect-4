using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startButton : MonoBehaviour
{
    public SceneAsset sceneToLoad;
    public void OnMouseDown()
    {
        string sceneName = sceneToLoad != null ? sceneToLoad.name : "";
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
