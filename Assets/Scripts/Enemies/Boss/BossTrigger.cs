using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] private EnemyTriggerForActivation bossActivation;

    [SerializeField] private Animation shortcutDoor;

    private void OnTriggerEnter(Collider other)
    {
        bossActivation.TriggerEnemy();
        this.gameObject.SetActive(false);
        shortcutDoor.Play();
    }
}
