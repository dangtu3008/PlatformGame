using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private bool gamePaused;

    [SerializeField] private PlayerController playerController;

    //Action
    private Vector2 direction = Vector2.zero;
    private bool isAttack = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    //Stats Player
    [SerializeField] private int maxHP;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    private int currentHP;
    [SerializeField] private Text txtCurrentHP;

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

    private void Start()
    {
        this.gamePaused = false;
        this.playerController = gameObject.GetComponentInParent<PlayerController>();
        this.boxCollider = gameObject.GetComponent<BoxCollider2D>();
        this.rb = gameObject.GetComponent<Rigidbody2D>();
        this.animator = gameObject.GetComponent<Animator>();
        this.currentHP = this.maxHP;
        this.slider = GameObject.Find("HpBar").GetComponent<Slider>();
        this.fill = GameObject.Find("Fill").GetComponent<Image>();
        this.txtCurrentHP = GameObject.Find("currentHP").GetComponent<Text>();

        SetMaxHealth(this.maxHP);
    }

    private void Update()
    {
        if (!gamePaused)
            MoveCharacter();
    }

    private void MoveCharacter()
    {
        Run();
        Jump();
        Attack();
        CheckTrap();
        CheckClimb();
    }

    private void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    private void Run()
    {
        this.direction.x = Input.GetAxisRaw("Horizontal");

        if (isAttack) return;
        rb.velocity = new Vector2(direction.x * 10f, rb.velocity.y);

        if (this.direction.x < 0f)
        {
            // isAttack = false;
            animator.SetFloat("Run", Mathf.Abs(this.direction.x));
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (this.direction.x > 0f)
        {
            // isAttack = false;
            animator.SetFloat("Run", this.direction.x);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else animator.SetFloat("Run", this.direction.x);

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enermy"))
        {
            currentHP -= 1;
            SetHealth(currentHP);
            txtCurrentHP.text = currentHP * 10 + "%";
            if (currentHP <= 0)
            {
                IsDead();
                return;
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isAttack = true;
            animator.SetTrigger("Attack");
        }
        isAttack = false;

    }

    private void Jump()
    {
        // if (isAttack) return;
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
            rb.velocity = Vector2.zero;
            currentHP = 0;
            txtCurrentHP.text = "0%";
            SetHealth(currentHP);
            IsDead();
        }
    }

    private void IsDead()
    {
        gamePaused = true;
        animator.SetBool("Die", true);
        Invoke("ReplayMenu", 0.5f);
    }

    private void ReplayMenu()
    {
        gamePaused = true;
        this.replayMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}
