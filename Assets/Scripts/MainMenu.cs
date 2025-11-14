using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSequence
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene("Scene" + index, LoadSceneMode.Single);
    }

    public static void LoadPauseSceneAdditive()
    {
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }

    public static void UnloadPauseScene()
    {
        SceneManager.UnloadSceneAsync("Pause");
    }
}

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
		Time.timeScale = 1;
		try
		{
			if (EventBus.Instance != null)
			{
				EventBus.Instance.Invoke(new EndSignal());
			}
		}
		catch
		{
		}
		SceneManager.LoadScene(0);
	}

    public void ResumeGame(){
        Time.timeScale = 1;
		SceneSequence.UnloadPauseScene();
	}

    public void PauseGame(){
        Time.timeScale = 0;
		SceneSequence.LoadPauseSceneAdditive();
	}
}
