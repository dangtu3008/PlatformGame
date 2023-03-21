using UnityEngine;

public class GuideManager : MonoBehaviour
{
    [SerializeField] GameObject guide;

    public void Guide()
    {
        this.guide.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGuide()
    {
        this.guide.SetActive(false);
        Time.timeScale = 1f;
    }
}
