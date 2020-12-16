using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartAnimation : MonoBehaviour
{
	[SerializeField] Sprite[] heartSprites;
	private int fps = 40;

    // Update is called once per frame
    void Update()
    {
		this.GetComponentInChildren<Image>().sprite = heartSprites[(int)(Time.time * fps) % heartSprites.Length];
	}
}
