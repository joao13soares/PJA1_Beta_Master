using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(AudioSource))]

public class EnemyTriggerForActivation : MonoBehaviour
{

    private AudioSource enemyAudioSourceComponent;
    [SerializeField] private AudioClip enemyActivationSound;
    
    private Enemy enemyScript;
    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();
        enemyAudioSourceComponent = GetComponent<AudioSource>();


    }

    public void TriggerEnemy()
    {
        enemyAudioSourceComponent.clip = enemyActivationSound;
        enemyAudioSourceComponent.Play();

        enemyScript.enabled = true;

    }
    
    
    
    
}
