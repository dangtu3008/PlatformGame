using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEvent : MonoBehaviour
{
    [SerializeField] private Animator chestAnimator;

    private void Start()
    {
        this.chestAnimator = GameObject.Find("Chest").GetComponent<Animator>();
    }

    // Chest
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Chest"))
        {
            // if (Input.GetKeyDown(KeyCode.Z))
            chestAnimator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Chest"))
        {
            chestAnimator.SetBool("isOpen", false);
        }
    }

    // Moving Platform
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(col.transform);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(col.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
}
