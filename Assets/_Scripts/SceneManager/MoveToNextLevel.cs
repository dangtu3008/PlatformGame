using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    [SerializeField] private int nextSceneLoad;
    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (nextSceneLoad > 5)
            {
                SceneManager.LoadScene("completeAllLevel");
                return;
            }
            SceneManager.LoadScene(nextSceneLoad);
        }

        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
            Debug.Log("Level at " + nextSceneLoad);
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
    }
}
