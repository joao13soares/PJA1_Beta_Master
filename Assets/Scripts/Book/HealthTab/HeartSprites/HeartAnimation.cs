using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartAnimation : MonoBehaviour
{
	[SerializeField] Sprite[] heartSprites;
	[SerializeField] private PlayerHealthManager playerHealthScript;

	private int lowHeartBeatFps = 40;
	private int highHeartBeatFps = 220;
	private int currentFps = 0;

	private Image heartImage;

	private void Awake()
	{
		heartImage = GetComponent<Image>();
		
		 currentFps = lowHeartBeatFps;
		 playerHealthScript.lowHpEvent += ChangeForFastHeartBeat;
		 playerHealthScript.highHPEvent += ChangeForSlowHeartBeat;
	}

	// Update is called once per frame
    void Update()
    {
		heartImage.sprite = heartSprites[(int)(Time.time * currentFps) % heartSprites.Length];
	}

    private void ChangeForFastHeartBeat()
    {
	    currentFps = highHeartBeatFps;
    }

    private void ChangeForSlowHeartBeat()
    {
	    currentFps = lowHeartBeatFps;
    }
}
