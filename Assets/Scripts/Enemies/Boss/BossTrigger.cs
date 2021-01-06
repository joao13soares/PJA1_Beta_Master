using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private EnemyTriggerForActivation bossActivation;


    private void OnTriggerEnter(Collider other)
    {
        bossActivation.TriggerEnemy();
    }
}
