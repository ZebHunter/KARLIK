using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame(){
        Application.Quit();
    }

	public void GoToMenu(){
		SceneManager.LoadScene(0);
	}

    public void ResumeGame(){
        Time.timeScale = 1;
		SceneManager.LoadScene(1);
	}

    public void PauseGame(){
        Time.timeScale = 0;
		SceneManager.LoadScene(4);
	}
}
