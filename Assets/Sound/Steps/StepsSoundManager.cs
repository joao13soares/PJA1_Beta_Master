using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsSoundManager : MonoBehaviour
{
	public AudioClip[] stepsAudioClips;
	
    [SerializeField] private AudioSource audioSource;
	
	private Dictionary<string, StepsSound> stepsSounds;
    
    string currentFloorType;

    void Start()
    {
        

        stepsSounds = new Dictionary<string, StepsSound>();
		stepsSounds.Add("tyle", new StepsSound("tyle", stepsAudioClips[0]));
		stepsSounds.Add("wood", new StepsSound("wood", stepsAudioClips[1]));
		// stepsSounds.Add("door", new StepsSound("door", stepsAudioClips[2]));

        currentFloorType = "tyle";
    }
	
	public void PlayAudioSample()
	{
		try
		{
			stepsSounds[currentFloorType].PlayAudioSample(audioSource);
		}
		catch (KeyNotFoundException e)
		{
			Debug.Log(e, this);
		}
	}

    public void UpdateCurrentFloorType(string floorType)
    {
        currentFloorType = floorType;
    }
}

public class StepsSound
{
	private AudioClip stepsAudioClip;
	private List<AudioSample> audioSamples;
	private int currentStepIndex;
	
	public StepsSound(string floorType, AudioClip audioClip)
	{
		stepsAudioClip = audioClip;
		audioSamples = new List<AudioSample>();
		currentStepIndex = 0;
		
		switch (floorType)
		{
			case "tyle":
				AddTyleAudioSample(audioSamples);
				break;
				
			case "wood":
				AddWoodAudioSample(audioSamples);
				break;
				
			case "door":
				AddDoorCreackingsAudioSample(audioSamples);
				break;
			default:
				break;
		}
	}
	
	public void PlayAudioSample(AudioSource audioSource)
    {
        audioSource.clip = stepsAudioClip;

        audioSource.time = audioSamples[currentStepIndex].StartTime;
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime + (audioSamples[currentStepIndex].EndTime - audioSamples[currentStepIndex].StartTime));

        UpdateCurrentIndex();
    }

    private void UpdateCurrentIndex()
    {
        if (currentStepIndex >= audioSamples.Count - 1)
            currentStepIndex = 0;
        else
            currentStepIndex++;
    }
	
	private void AddTyleAudioSample(List<AudioSample> list)
	{
		// walking_on_tile AudioSource
        list.Add(new AudioSample(0, 0.700f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 1.350f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 2.000f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 2.700f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 3.350f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 4.000f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 4.700f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 5.400f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 6.100f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 6.750f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 7.400f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 8.100f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 8.800f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 9.500f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 10.200f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 10.900f));
	}
	
	private void AddWoodAudioSample(List<AudioSample> list)
	{
		// walking_on_wood AudioSource
		list.Add(new AudioSample(0, 0.700f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 1.350f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 1.950f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 2.700f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 3.460f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 4.000f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 4.800f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 5.500f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 6.000f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 6.800f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 7.460f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 8.100f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 8.800f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 9.500f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 10.000f));
        list.Add(new AudioSample(list[list.Count - 1].EndTime, 10.700f));
	}
	
	
	private void AddDoorCreackingsAudioSample(List<AudioSample> list)
	{
		// door_creakings AudioSamples
		list.Add(new AudioSample(0, 3.000f));
		list.Add(new AudioSample(list[list.Count - 1].EndTime, 7.000f));
		list.Add(new AudioSample(list[list.Count - 1].EndTime, 9.500f));
	}
}

public class AudioSample
{
    public float StartTime { get; }
    public float EndTime { get; }

    public AudioSample(float startTime, float endTime)
    {
        this.StartTime = startTime;
        this.EndTime = endTime;
    }
}