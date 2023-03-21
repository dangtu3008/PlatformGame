using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int levelToLoad;
    private void Start()
    {
        this.animator = GameObject.Find("LevelChanger").GetComponent<Animator>();
    }

    public void FadeToNextLevel(int levelIndex)
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        this.levelToLoad = levelIndex;
        this.animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("Level_" + this.levelToLoad);
    }

}
