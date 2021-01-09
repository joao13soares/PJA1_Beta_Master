using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour, IInteractable
{

    private int endGameScreen = 3;
    public void OnRaycastSelect()
    {
        SceneManager.LoadScene(endGameScreen);
    }
}
