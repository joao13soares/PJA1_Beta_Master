using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject loadingScreen, MainMenuGO, ControlsMenu;

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
        MainMenuGO.SetActive(false);

        ControlsMenu.SetActive(true);


    }

    public void ControlsBack()
    {
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
        // Sets loadingScreen
        loadingScreen.SetActive(true);

        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!loading.isDone)
        {

            yield return null;
        }
    }

   



   
}
