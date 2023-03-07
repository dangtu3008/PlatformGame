using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    private Vector2 direction = Vector2.zero;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    //Animation
    [SerializeField] private Animator animator;

    //Check on ground
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask ground;
    //Climb ladder
    [SerializeField] private LayerMask climbableLayerMask;
    [SerializeField] private LayerMask trapLayerMask;
    [SerializeField] private float climbSpeed;
    [SerializeField] GameObject replayMenu;
    [SerializeField] GameObject teleport;

    private void Start()
    {
        this.rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        this.boxCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        this.animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        MoveCharacter();
    }

    protected void MoveCharacter()
    {
        this.direction.x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction.x * 10f, rb.velocity.y);

        Run(direction.x);
        Jump();
        Attack();
        CheckTrap();
        CheckClimb();
    }

    protected void CheckTeleport()
    {
        // Test condition
        if (Input.GetKeyDown(KeyCode.Z))
        {
            teleport.SetActive(true);
        }
    }

    protected void Run(float xdirection)
    {
        if (xdirection < 0f)
        {
            animator.SetFloat("Run", Mathf.Abs(xdirection));
            transform.parent.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else if (xdirection > 0f)
        {
            animator.SetFloat("Run", xdirection);
            transform.parent.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else animator.SetFloat("Run", xdirection);

    }

    protected void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attack");
        }
    }

    protected void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && CheckIsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private bool CheckIsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    private void CheckClimb()
    {
        if (boxCollider.IsTouchingLayers(climbableLayerMask))
        {
            Vector2 veclocity = rb.velocity;
            veclocity.y = climbSpeed * Input.GetAxisRaw("Vertical");
            rb.velocity = veclocity;
            rb.gravityScale = 0;
        }
        else rb.gravityScale = 6f;
    }

    private void CheckTrap()
    {
        if (boxCollider.IsTouchingLayers(trapLayerMask))
        {
            animator.SetBool("Die", true);
            Invoke("ReplayMenu", 1f);
        }
    }

    private void ReplayMenu()
    {
        this.replayMenu.SetActive(true);
        Time.timeScale = 0f;
    }


}
