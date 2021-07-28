using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExplanationStep
{
    public GameObject text;
    public float time;
    public bool showned;

    public void SetShowned(bool boolean)
    {
        showned = boolean;
    }
}

public class LobbyMessageController : MonoBehaviour
{
    public bool explanationWanted = true;
    public bool endReached = false;

    public List<ExplanationStep> explanationSteps = new List<ExplanationStep>();
    public GameObject background;
    public bool shouldBackgroundBeShowned = true;

    private IEnumerator coroutine;


    private void FixedUpdate()
    {
        if (explanationWanted)
        {
            if (!endReached)
            {
                ExplanationStep ep = GetNextExplanationStep();
                coroutine = DeactiveTextAfter(ep);
                StartCoroutine(coroutine);
                shouldBackgroundBeShowned = true;
            }
            else
            {
                shouldBackgroundBeShowned = false;
            }
        }

        if (shouldBackgroundBeShowned)
        {
            background.SetActive(true);
        } else
        {
            background.SetActive(false);
        }
    }

    private IEnumerator DeactiveTextAfter(ExplanationStep explanationStep)
    {
        if(explanationStep != null)
        {
            explanationStep.text.SetActive(true);
            yield return new WaitForSeconds(explanationStep.time);
            explanationStep.text.SetActive(false);
            explanationStep.SetShowned(true);
        }
    }

    private ExplanationStep GetNextExplanationStep()
    {
        for (int i = 0; i < explanationSteps.Count; i++)
        {
            if (!explanationSteps[i].showned)
            {
                return explanationSteps[i];
            }
        }
        endReached = true;

        return null;
    }

    public void PlayHelpsAgain()
    {
        explanationWanted = false;
        endReached = false;
        
        for (int i = 1; i < explanationSteps.Count; i++)
        {
            explanationSteps[i].SetShowned(false);
        }

        explanationWanted = true;
    }
}

