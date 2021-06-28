using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEnterScript : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [SerializeField] private float howLongWait = 1f;
    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private bool doorsOpening = false;
    [SerializeField] private float doorsSpeed = 0.1f;
    


    public void PlayScene()
    {
        Debug.Log("Playing scene: " + sceneName);
        doorsOpening = true;


        //StartCoroutine(WaitCoroutine());


        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(this.sceneName);
        
        if(loadingOperation.isDone)
        {
            loadingScreen.SetActive(false);
        }
    }

    
    void Update()
    {
        if(doorsOpening)
        {
            leftDoor.transform.position = new Vector3(leftDoor.transform.position.x, leftDoor.transform.position.y, leftDoor.transform.position.z - doorsSpeed);
            rightDoor.transform.position = new Vector3(rightDoor.transform.position.x, rightDoor.transform.position.y, rightDoor.transform.position.z + doorsSpeed);
        }
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(howLongWait);
    }
}
