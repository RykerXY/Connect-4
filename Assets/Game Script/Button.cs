using TMPro;
using UnityEngine;
using UnityEngine.UI; // To interact with Button
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonClickHandler : MonoBehaviour
{
    public SceneAsset sceneToLoad;

    // This function will be called when the button is clicked
    public void OnButtonClick()
    {
        // Change the text when button is clicked
        string sceneName = sceneToLoad != null ? sceneToLoad.name : "";
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
