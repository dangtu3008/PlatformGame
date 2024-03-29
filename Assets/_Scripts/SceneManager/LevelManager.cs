using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private GameObject lvlChanger;
    private void Start()
    {
        this.levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        this.lvlChanger = GameObject.Find("LevelChanger");

        Invoke("DeActiveFade", 1f);

        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        for (int i = 0; i < this.levelButtons.Length; i++)
        {
            if (i + 2 > levelAt)
                this.levelButtons[i].interactable = false;
        }
    }

    public void LoadLevel(int index)
    {

        this.levelChanger.FadeToLevel(index);
        this.levelChanger.OnFadeComplete();
    }

    public void DeActiveFade()
    {
        this.lvlChanger.SetActive(false);
    }

}
