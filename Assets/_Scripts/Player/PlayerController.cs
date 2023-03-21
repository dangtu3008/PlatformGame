using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private BoxCollider2D attackCollider;
    [SerializeField] private CheckEvent checkEvent;
    [SerializeField] private Animator animator;

    void Start()
    {
        this.movement = gameObject.GetComponentInChildren<PlayerMovement>();
        this.attackCollider = gameObject.GetComponentInChildren<BoxCollider2D>();
        this.checkEvent = gameObject.GetComponentInChildren<CheckEvent>();
        this.animator = gameObject.GetComponentInChildren<Animator>();
    }

}
