using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAnimation : MonoBehaviour
{

    public enum State
    {
        Idle,
        Run,
        Attack,
        Hit
    }

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigibody2d;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private State state;

    [SerializeField] private int hpEnermy;
    [Header("Object Pooling")]
    [SerializeField] private BulletPool bulletPool;

    private int direction = 1;
    private Vector3 startPosition;
    private bool getHit;
    private float getHitTime;

    void Start()
    {
        this.animator = gameObject.GetComponent<Animator>();
        this.rigibody2d = gameObject.GetComponent<Rigidbody2D>();
        this.startPosition = transform.position;

        SetState(State.Idle);
        SetDirection(1);
        StartCoroutine(UpdateAI());
    }

    private void Update()
    {
        if (getHit)
        {
            getHitTime -= Time.deltaTime;
            if (getHitTime <= 0)
                getHit = false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (!Application.isPlaying)
            startPosition = transform.position;
        Gizmos.DrawLine(new Vector2(startPosition.x - distance, startPosition.y),
        new Vector2(startPosition.x + distance, startPosition.y));
    }

    private IEnumerator UpdateAI()
    {
        while (true)
        {
            if (state == State.Idle)
            {
                yield return new WaitForSeconds(3f);
                SetState(State.Run);
            }
            else if (state == State.Run)
            {
                float curDistance = Vector2.Distance(startPosition, transform.position);
                if (curDistance > distance)
                {
                    if (transform.position.x > startPosition.x && direction == 1)
                    {
                        PlayIdleAnim();
                        yield return new WaitForSeconds(5f);
                        PlayRunAnim();
                        SetDirection(-1);
                    }
                    else if (transform.position.x < startPosition.x && direction == -1)
                    {
                        PlayIdleAnim();
                        yield return new WaitForSeconds(5f);
                        PlayRunAnim();
                        SetDirection(1);
                    }
                }
                rigibody2d.velocity = new Vector2(direction * speed, rigibody2d.velocity.y);
            }
            else if (state == State.Attack)
            {

            }
            else if (state == State.Hit)
            {
                yield return new WaitForSeconds(0.5f);
                getHit = false;
                SetState(State.Run);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (getHit) return;
        if (col.CompareTag("Player"))
        {
            SetState(State.Attack);
            Debug.Log("Player enter Collider");

        }

        if (col.CompareTag("Attack"))
        {
            getHit = true;
            SetState(State.Hit);
            getHitTime = 1f;
            hpEnermy -= 1;
            if (hpEnermy <= 0)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {

    }

    private void OnTriggerExit2D(Collider2D col)
    {

    }

    private void SetDirection(int direction)
    {
        this.direction = direction;
        transform.localScale = new Vector3(-this.direction, 1, 1);
    }

    private void SetState(State state)
    {
        this.state = state;
        switch (state)
        {
            case State.Idle:
                PlayIdleAnim();
                break;
            case State.Run:
                PlayRunAnim();
                break;
            case State.Attack:
                PlayAttackAnim();
                break;
            case State.Hit:
                PlayHitAnim();
                break;
        }
    }

    public void PlayIdleAnim()
    {
        this.animator.SetTrigger("Change");
        this.animator.SetInteger("State", 1);
    }

    public void PlayRunAnim()
    {
        this.animator.SetTrigger("Change");
        this.animator.SetInteger("State", 2);
    }

    public void PlayAttackAnim()
    {
        this.animator.SetTrigger("Change");
        this.animator.SetInteger("State", 3);
    }

    public void PlayHitAnim()
    {
        this.animator.SetTrigger("Change");
        this.animator.SetInteger("State", 4);
    }
}
