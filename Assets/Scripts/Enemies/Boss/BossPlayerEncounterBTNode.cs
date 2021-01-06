using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(AudioSource))]
public class BossPlayerEncounterBTNode : BTNode
{
    private Animation enemyAnimationComponent;
    [SerializeField] private AnimationClip encounterAnimation;

    private AudioSource enemyAudioSourceComponent;
    [SerializeField] private AudioClip encounterAudio;

    private bool hasStarted;
    private bool isRunning;
    protected override void Awake()
    {
        enemyAnimationComponent = GetComponent<Animation>();
        enemyAudioSourceComponent = GetComponent<AudioSource>();

        hasStarted = false;
        isRunning = false;
        
        base.Awake();
    }

    public override Result Execute()
    {

        if (!hasStarted)
        {
          StartCoroutine( PlayingEncounterAudioAnimation()) ;
            hasStarted = true;
        }
        else if (hasStarted && isRunning) return Result.Running;
        else if (hasStarted && !isRunning)
        {
            hasStarted = false;
            return Result.Success;
        }



        return Result.Failure;
    }

    IEnumerator PlayingEncounterAudioAnimation()
    {
        isRunning = true;
        enemyAnimationComponent.clip = encounterAnimation;
        enemyAnimationComponent.Play();
        enemyAudioSourceComponent.clip = encounterAudio;
        enemyAudioSourceComponent.Play();
        
        yield return new WaitForSeconds(encounterAnimation.length);
        isRunning = false;






    }


   
}
