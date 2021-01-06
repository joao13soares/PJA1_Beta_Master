using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthAreaOpenDoor : MonoBehaviour
{
    private Animation doorAnimationComponent;

    [SerializeField] private Enemy bossEnemy;
    private void Awake()
    {
        doorAnimationComponent = GetComponent<Animation>();

        bossEnemy.died += OpenDoor;
    }

    void OpenDoor()
    {
        doorAnimationComponent.Play();
    }
}
