using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        this.pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        this.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Replay()
    {
        this.pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChooseLevel()
    {
        SceneManager.LoadScene("LevelSelection");
        Time.timeScale = 1f;
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
        Time.timeScale = 1f;
    }
}
