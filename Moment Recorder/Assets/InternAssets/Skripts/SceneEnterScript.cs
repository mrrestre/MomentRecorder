using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEnterScript : MonoBehaviour
{
    [Header("Scene to Load")]

    // Name of the Scene to be loaded
    [SerializeField] private string sceneName;

    [SerializeField] private float howLongWait = 1f;

    // Loading screen to be loaded while a scene is being loaded
    [SerializeField] private GameObject loadingScreen;

    public void PlayScene()
    {
        Debug.Log("Playing scene: " + sceneName);

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(this.sceneName);

        while (!loadingOperation.isDone)
        {
            loadingScreen.SetActive(true);
            yield return null;
        }
    }
}
