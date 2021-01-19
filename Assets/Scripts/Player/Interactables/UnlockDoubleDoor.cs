using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnlockDoubleDoor : MonoBehaviour, IInteractable
{

    [SerializeField] private Animation doubleDoorToOpen;
    
    private AudioSource buttonClickAudioSource;


    private void Awake()
    {
        buttonClickAudioSource = GetComponent<AudioSource>();
    }

    public void OnRaycastSelect()
    {
        doubleDoorToOpen.Play();
        this.enabled = false;
        
        buttonClickAudioSource.Play();

    }
}
