using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorSound : MonoBehaviour
{
    private AudioSource doorAudioSource;

    private DragDoor correspondentDoor;

    private bool raisingVolume, loweringVolume;


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
        bool canChangeVolume = doorAudioSource.volume >= 0f && doorAudioSource.volume <= 1f;
        if (!canChangeVolume) return;
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
}