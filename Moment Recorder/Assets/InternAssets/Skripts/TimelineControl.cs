using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineControl : MonoBehaviour
{
    [Header("Timeline Components")]

    // Where the Timeline of the scene is stored
    [SerializeField] private PlayableDirector playableDirector;

    // Defines the amount of time that does backwards each frame (If the Backward Button is beeing pressed)
    [SerializeField] private float backwardsStep = 0.035f;

    // Defines until which moment of the timeline the player is able to go back
    [SerializeField] private float sceneStartTime = 1f;

    [SerializeField] private bool isPlaying = false;
    [SerializeField] private bool isRewinding = false;

    [SerializeField] private bool hasStarted = false;
    [SerializeField] private bool hasFinished = false;

    [Header("Input Components")]

    [SerializeField] private OVRInput.Button backwardsButton;
    [SerializeField] private OVRInput.Button pauseButton;

    [Header("Back to Lobby")]

    [SerializeField] private OVRInput.Button backToLobby;
    [SerializeField] private bool backToLobbyPressed = false;
    [SerializeField] private string lobby = "Lobby";
    [SerializeField] private GameObject loadingScreen;

    [Header("UI Control")]

    [SerializeField] private GameObject UIplayButton;
    [SerializeField] private GameObject UIpauseButton;
    [SerializeField] private GameObject UIrewindButton;

    [Header("UI Timeline")]
    [SerializeField] private GameObject UITimeline;
    private Slider UISlider;

    [Header("Help")]

    [SerializeField] private OVRInput.Button helpButton;
    [SerializeField] public SceneMessageController sceneMessageController;

    private void Start()
    {
        UISlider = UITimeline.GetComponent<Slider>();
        UISlider.maxValue = (float)playableDirector.duration;
        UISlider.value = 0;
    }

    void Update()
    {
        if(!hasFinished)
        {
            // Check if the pause button is pressed and toggle between playing and paused
            if (OVRInput.GetDown(pauseButton))
            {
                if (!hasStarted)
                    hasStarted = true;
                if (isPlaying)
                    isPlaying = false;
                else if (!isPlaying)
                    isPlaying = true;
            }

            // Check if the Rewind button is pressed and rewind so long until the button is released
            if (OVRInput.GetDown(backwardsButton)) isRewinding = true;
            if (OVRInput.GetUp(backwardsButton)) isRewinding = false;

            // Check if the help button is pressed
            if (OVRInput.GetDown(helpButton))
            {
                sceneMessageController.PlayHelpsAgain();
            }

            // Go back to the lobby
            if (OVRInput.GetDown(backToLobby))
            {
                backToLobbyPressed = true;
            }

            //Set the current time in the timeline
            UISlider.value = (float)playableDirector.time;


            if (playableDirector.time == playableDirector.duration)
            {
                hasFinished = true;
                hasStarted = false;
                isPlaying = false;
                playableDirector.Stop();
            }
        }
        else if (hasFinished)
        {
            if (OVRInput.GetDown(pauseButton))
            {
                playableDirector.time = 0;
                hasFinished = false;
                sceneMessageController.SceneEndHide();
            }

            // Go back to the lobby
            if (OVRInput.GetDown(backToLobby))
            {
                backToLobbyPressed = true;
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (hasStarted)
        {
            // Play only if the variable is Playing is true
            if (isPlaying)
            {
                playableDirector.Play();
                UIplayButton.SetActive(true);
                UIpauseButton.SetActive(false);
                UIrewindButton.SetActive(false);
            }

            else if (!isPlaying)
            {
                playableDirector.Pause();
                UIplayButton.SetActive(false);
                UIpauseButton.SetActive(true);
                UIrewindButton.SetActive(false);
            }


            // This is the same as the button "BackwardButton" being pressed
            if (isRewinding)
            {
                UIplayButton.SetActive(false);
                UIpauseButton.SetActive(false);
                UIrewindButton.SetActive(true);

                // For seeing how the time goes backwards
                if (!isPlaying) playableDirector.Play();

                // Stop rewinding at a given moment (Scene Start)
                if (playableDirector.time >= sceneStartTime) playableDirector.time -= backwardsStep;
            }
        }
        else if (!hasStarted)
        {
            UIplayButton.SetActive(false);
            UIpauseButton.SetActive(true);
            UIrewindButton.SetActive(false);
        }

        if (backToLobbyPressed)
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadLobby());
        }

        if (hasFinished)
        {
            sceneMessageController.SceneEndShow();
        }
    }

    private IEnumerator LoadLobby()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(lobby);
    }

}
