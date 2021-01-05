using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForSpawn : MonoBehaviour,IInteractable
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Light lightForWrongFeedback;

    private bool alreadyFailed = false;
    
    public void OnRaycastSelect()
    {
        if (!alreadyFailed)
        {
            // for(int i = 0;i<3;i++)
            enemySpawner.SpawnEnemy();
            alreadyFailed = true;

        }
        
        lightForWrongFeedback.enabled = true;
    }
}
