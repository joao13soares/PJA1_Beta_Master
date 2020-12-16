using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowArea : MonoBehaviour
{
	[SerializeField] GameObject mapArea;

	[SerializeField] private AudioSource drawingAudioSource;
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			
			mapArea.SetActive(true);
			// on drawing map function, insert this line
			StartCoroutine(PlayWritingSound(1.5f));
		}
	}
	
	

	IEnumerator PlayWritingSound(float drawingDuration)
	{
		if(!drawingAudioSource.isPlaying)
		{
			drawingAudioSource.Play();
			yield return new WaitForSeconds(drawingDuration);
			drawingAudioSource.Pause();

			this.gameObject.SetActive(false);
		}
	}
	
}
