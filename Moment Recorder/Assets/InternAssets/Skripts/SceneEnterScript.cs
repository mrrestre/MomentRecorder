using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEnterScript : MonoBehaviour
{
    [Header("Scene to Load")]

    // Name of the Scene to be loaded
    [SerializeField] private string sceneName;

    [Header("Loading Screen")]

    // Loading screen to be loaded while a scene is being loaded
    [SerializeField] private GameObject loadingScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayScene();
        }
    }


    public void PlayScene()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(this.sceneName);
    }
}
