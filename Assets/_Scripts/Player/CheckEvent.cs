using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEvent : MonoBehaviour
{
    // [SerializeField] private Animator chestAnimator;
    [SerializeField] private GameObject stoneTeleport;
    [SerializeField] private GameObject teleport;

    // [SerializeField] private GameObject completePanel;
    private Animator stoneAnimator;


    private void Start()
    {
        // if (chestAnimator != null) return;
        // else this.chestAnimator = GameObject.Find("Chest").GetComponent<Animator>();
        if (stoneAnimator != null) return;
        else this.stoneTeleport = GameObject.Find("StoneTeleport");
        if (stoneAnimator != null) return;
        else this.stoneAnimator = this.stoneTeleport.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // if (col.CompareTag("Chest"))
        // {
        //     chestAnimator.SetBool("isOpen", true);
        //     this.stoneTeleport.SetActive(true);

        // }
        if (col.gameObject.CompareTag("StoneTeleport"))
        {
            this.stoneTeleport.SetActive(false);
            teleport.SetActive(true);
        }
    }

    // Moving Platform
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent.SetParent(col.transform);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent.SetParent(col.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent.SetParent(null);
        }
    }

}
