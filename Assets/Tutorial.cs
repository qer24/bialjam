using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private AutoTween endTutorialTween;
    [SerializeField] private Health[] tutorialEnemies;
    [SerializeField] private Volume postProcessing;
    [SerializeField] private float startTimeScale = 0.35f;
    [SerializeField] private CanvasGroup tutorialText;

    private bool endedTutorial;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.05f);

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LowPass", 1f);
        Time.timeScale = startTimeScale;
        postProcessing.weight = 1f;
    }

    private void Update()
    {
        if (endedTutorial) return;
        
        if (tutorialEnemies.Any(hp => hp.isDead))
        {
            endedTutorial = true;
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        endTutorialTween.Play(LeanTween.value(gameObject, val => {
            Time.timeScale = Mathf.Lerp(startTimeScale, 1f, val);
            postProcessing.weight = 1 - val;
            tutorialText.alpha = 1 - val;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LowPass", 1 - val);
        }, 0f, 1f, endTutorialTween.time)).setIgnoreTimeScale(true);
    }
}