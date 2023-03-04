using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        int checkLevel = 2;
        // if pass level ... checkLevel += 1...
        int levelAt = PlayerPrefs.GetInt("levelAt", checkLevel);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 2 > levelAt)
                levelButtons[i].interactable = false;
        }
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene("Level_" + index);
    }
}
