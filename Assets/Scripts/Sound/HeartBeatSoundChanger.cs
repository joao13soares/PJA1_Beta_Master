using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class HeartBeatSoundChanger : MonoBehaviour
{
    [SerializeField]private PlayerHealthManager playerHealthScript;

    private AudioSource heartbeatAudioSource;
    [SerializeField] private AudioClip fastHeartbeat;
    [SerializeField] private AudioClip slowHeartbeat;

    // Start is called before the first frame update
    void Start()
    {
        heartbeatAudioSource = GetComponent<AudioSource>();
        playerHealthScript.lowHpEvent += ChangeForFastHeartBeat;
        playerHealthScript.highHPEvent += ChangeForSlowHeartBeat;

    }

    // Update is called once per frame
    void ChangeForFastHeartBeat()
    {
        heartbeatAudioSource.clip = fastHeartbeat;
        heartbeatAudioSource.Play();
    }

    void ChangeForSlowHeartBeat()
    {
        heartbeatAudioSource.clip = slowHeartbeat;
        heartbeatAudioSource.Play();

    }
}
