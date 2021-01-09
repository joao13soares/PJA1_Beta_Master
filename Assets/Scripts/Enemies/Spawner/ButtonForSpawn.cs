using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForSpawn : MonoBehaviour,IInteractable
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Light lightForWrongFeedback;
    
    private AudioSource buttonClickAudioSource;


    private bool alreadyFailed = false;


    private void Awake()
    {
        buttonClickAudioSource = GetComponent<AudioSource>();
    }

    public void OnRaycastSelect()
    {
        if (!alreadyFailed)
        {
            // for(int i = 0;i<3;i++)
            enemySpawner.SpawnEnemy();
            alreadyFailed = true;

        }
        
        lightForWrongFeedback.enabled = true;
        
        
        buttonClickAudioSource.Play();
        
    }
}
