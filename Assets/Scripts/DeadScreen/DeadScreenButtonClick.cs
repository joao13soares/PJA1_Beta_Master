
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreenButtonClick : MonoBehaviour
{

    private int gameScene = 1;
    
    
    public void RestartButton()
    {
        SceneManager.LoadScene(0);

    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    
}
