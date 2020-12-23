using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorSound : MonoBehaviour
{
    private AudioSource doorAudioSource;

    [SerializeField] private List<AudioClip> doorAudioClips;


    private DragDoor correspondentDoor;

    private bool raisingVolume, loweringVolume;


    private bool canStartCuroutine => !raisingVolume && !loweringVolume;
    
    private float amountToAdd;
    private void Awake()
    {
        correspondentDoor = this.GetComponent<DragDoor>();
        doorAudioSource = this.GetComponent<AudioSource>();


        correspondentDoor.moved += PlayClip;
        correspondentDoor.stopped += PauseClip;

        
        doorAudioSource.Play();
        doorAudioSource.loop = true;

        raisingVolume = false;
        loweringVolume = false;

        amountToAdd = -2f;
    }


    
     void Update()
    {
        
        doorAudioSource.volume += amountToAdd * Time.deltaTime;

    }

    void PlayClip()
    {
        amountToAdd = 1f;
    }

    void PauseClip()
    {
        amountToAdd = -2f;
    }

    IEnumerator RaiseVolume()
    {
        raisingVolume = true;
        while (doorAudioSource.volume < 1)
        {
            doorAudioSource.volume += 0.1f;
            yield return new WaitForSeconds(0.1f); 

        }

        raisingVolume = false;
    }

    IEnumerator LowerVolume()
    {
        loweringVolume = true;
        while (doorAudioSource.volume > 0)
        {
            doorAudioSource.volume -= 0.2f;
            yield return new WaitForSeconds(0.2f); 

        }

        loweringVolume = false;

    }

    
}