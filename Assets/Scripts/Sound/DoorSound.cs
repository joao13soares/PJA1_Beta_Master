using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorSound : MonoBehaviour
{
    private AudioSource doorAudioSource;

    [SerializeField] private List<AudioClip> doorAudioClips;


    private DragDoor correspondentDoor;


    private void Awake()
    {
        correspondentDoor = this.GetComponent<DragDoor>();
        doorAudioSource = this.GetComponent<AudioSource>();


        correspondentDoor.moved += PlayClip;
        correspondentDoor.stopped += PauseClip;
    }


    void PlayClip()
    {
        if (doorAudioSource.isPlaying) return;
        doorAudioSource.clip = doorAudioClips[Random.Range(0, doorAudioClips.Count - 1)];
        doorAudioSource.Play();
    }

    void PauseClip() => doorAudioSource.Pause();
}