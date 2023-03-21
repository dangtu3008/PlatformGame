using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void QuitGame()
    {
        Application.Quit();

        //Test on unity editor
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
