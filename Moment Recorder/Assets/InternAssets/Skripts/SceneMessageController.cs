using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneMessageController : MonoBehaviour
{
    public bool explanationWanted = true;
    public bool endReached = false;

    public List<ExplanationStep> explanationSteps = new List<ExplanationStep>();
    public GameObject messageBackground;

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
            }
        }

        if(endReached)
        {
            messageBackground.SetActive(false);
        }
        else
        {
            messageBackground.SetActive(true);
        }
    }

    private IEnumerator DeactiveTextAfter(ExplanationStep explanationStep)
    {
        if (explanationStep != null)
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

        for (int i = 0; i < explanationSteps.Count; i++)
        {
            explanationSteps[i].SetShowned(false);
        }

        explanationWanted = true;
    }
}

