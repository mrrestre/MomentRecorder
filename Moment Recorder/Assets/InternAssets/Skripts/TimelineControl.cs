using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineControl : MonoBehaviour
{
    [Header("Timeline Components")]

    // Where the Timeline of the scene is stored
    [SerializeField] private PlayableDirector playableDirector;

    // Defines the amount of time that does backwards each frame (If the Backward Button is beeing pressed)
    [SerializeField] private float backwardsStep = 0.035f;

    // Defines until which moment of the timeline the player is able to go back
    [SerializeField] private float sceneStartTime = 1f;

    [SerializeField] private bool isPlaying = true;
    [SerializeField] private bool isRewinding = false;

    [Header("Input Components")]

    [SerializeField] private OVRInput.Button backwardsButton;
    [SerializeField] private OVRInput.Button pauseButton;

    void Update()
    {
        // Check if the pause button is pressed and toggle between playing and paused
        if (OVRInput.GetDown(pauseButton))
        {
            if (isPlaying) isPlaying = false;
            else isPlaying = true;
        }

        // Check if the Rewind button is pressed and rewind so long until the button is released
        if (OVRInput.GetDown(backwardsButton))  isRewinding = true;
        if (OVRInput.GetUp  (backwardsButton))  isRewinding = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Play only if the variable is Playing is true
        if (isPlaying) playableDirector.Play();
        else if (!isPlaying) playableDirector.Pause();

        // This is the same as the button "BackwardButton" being pressed
        if (isRewinding)
        {
            // For seeing how the time goes backwards
            if (!isPlaying) playableDirector.Play();

            // Stop rewinding at a given moment (Scene Start)
            if (playableDirector.time >= sceneStartTime) playableDirector.time -= backwardsStep;
        }
    }
}
