using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneMessageController : MonoBehaviour
{
    public bool explanationWanted = true;
    public bool endReached = false;

    public List<ExplanationStep> explanationSteps = new List<ExplanationStep>();
    public GameObject messageBackground;
    public ExplanationStep sceneEnd;

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
            if(explanationWanted)
            {
                explanationStep.text.SetActive(true);
                yield return new WaitForSeconds(explanationStep.time);
                explanationStep.text.SetActive(false);
                explanationStep.SetShowned(true);
            }
            else
            {
                explanationStep.text.SetActive(false);
                explanationStep.SetShowned(true);
            }
        }
        else
        {
            messageBackground.SetActive(false);
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

    public void SceneEndShow()
    {
        messageBackground.SetActive(true);
        sceneEnd.text.SetActive(true);
    }

    public void SceneEndHide()
    {
        messageBackground.SetActive(false);
        sceneEnd.text.SetActive(false);
    }

    public void SkipInstruction()
    {
        for (int i = 0; i < explanationSteps.Count; i++)
        {
            explanationSteps[i].SetShowned(true);
        }

        StopCoroutine(coroutine);

        StopAllCoroutines();

        explanationWanted = false;
        endReached = true;
        messageBackground.SetActive(false);
    }
}

