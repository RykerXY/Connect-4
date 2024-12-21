using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonClickHandler : MonoBehaviour
{
    //public SceneAsset sceneToLoad;

    public void OnButtonClick()
    {
        SceneManager.LoadScene("Menu");
        /*string sceneName = sceneToLoad != null ? sceneToLoad.name : "";
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }*/
    }
}
