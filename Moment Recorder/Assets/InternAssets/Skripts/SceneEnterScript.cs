using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEnterScript : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void PlayScene()
    {
        SceneManager.LoadScene(this.sceneName);
    }
}
