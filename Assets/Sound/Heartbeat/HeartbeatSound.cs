using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatSound : MonoBehaviour
{
	public AudioClip[] heartbeatAudioClips;
	
    private AudioSource audioSource;

    int heartbeatFrequencyLevel;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        heartbeatFrequencyLevel = 1;
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayHeartbeat();
        }
    }

    void PlayHeartbeat()
    {
        audioSource.clip = heartbeatAudioClips[heartbeatFrequencyLevel - 1];

        audioSource.Play();
    }
    
    

    public void ChangeHeartbeatFrequency(int frequencyLevel)
    {
        if(frequencyLevel < 1 || frequencyLevel > 3) return;

        heartbeatFrequencyLevel = frequencyLevel;
    }
}
