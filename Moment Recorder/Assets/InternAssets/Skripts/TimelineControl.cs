using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineControl : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public bool isPlaying = true;

    public bool isRewinding = false;
    public float backwardsSteps = 0.1f;

    public OVRInput.Button backwardsButton;
    public OVRInput.Button pauseButton;


    // Start is called before the first frame update
    void Start()
    {
        if (OVRInput.GetDown(pauseButton))
        {
            if (isPlaying)
            {
                isPlaying = false;
            }
            else
            {
                isPlaying = true;
            }
        }

        if (OVRInput.GetDown(backwardsButton))
        {
            isRewinding = true;
        }
        if (OVRInput.GetUp(backwardsButton))
        {
            isRewinding = false;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isPlaying)
        {
            playableDirector.Play();
        }
        else if (!isPlaying)
        {
            playableDirector.Pause();
        }

        if (isRewinding)
        {
            if (!isPlaying)
            {
                playableDirector.Play();
            }

            if (playableDirector.time >= 1f)
            {
                playableDirector.time -= backwardsSteps;
            }

        }
    }
}
