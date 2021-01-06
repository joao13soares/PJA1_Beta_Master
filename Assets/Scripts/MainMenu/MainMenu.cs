using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
 
public class MainMenu : MonoBehaviour
{
 
    public GameObject loadingScreen, MainMenuGO, ControlsMenu;
    public Animation cameraAnim;
    public AnimationClip ControlsIn, ControlsOut;
    public Slider slider;
    public Text progressText;
 

    public void Play()
    {
        // Turns off bacground and MainMenu
        MainMenuGO.SetActive(false);
 
        // Loads level async
        StartCoroutine(LoadAsynchronously());
    }
 
    public void Controls()
    {
        // Turns off bacground and MainMenu
        //Apartment.SetActive(false);
        cameraAnim.clip = ControlsIn;
        cameraAnim.Play();
 
        MainMenuGO.SetActive(false);
        ControlsMenu.SetActive(true);
 
        
 
    }
 
    public void ControlsBack()
    {
        cameraAnim.clip = ControlsOut;
        cameraAnim.Play();
        ControlsMenu.SetActive(false);
 
        // Turns off bacground and MainMenu
        MainMenuGO.SetActive(true);
 
        
    }
 
    public void Quit()
    {
        Application.Quit();
    }
 
 
    // Load scene in background
    IEnumerator LoadAsynchronously()
    {
        // // Sets loadingScreen

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
 
   
 
 
 
   
}