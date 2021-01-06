using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using UnityEngine.SceneManagement;
 
public class MainMenu : MonoBehaviour
{
 
    public GameObject /*loadingScreen;*/ MainMenuGO, ControlsMenu;
    public Animation cameraAnim;
    public AnimationClip ControlsIn, ControlsOut;
 
 

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
        // loadingScreen.SetActive(true);
 
        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
 
        while (!loading.isDone)
        {
 
            yield return null;
        }
    }
 
   
 
 
 
   
}