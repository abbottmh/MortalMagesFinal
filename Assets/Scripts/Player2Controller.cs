using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animate;

    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    private bool facingRight = true;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;
    public GameManager gameManager;
    private bool deathState;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animate = gameObject.GetComponent<Animator>();
        moveSpeed = 1.5f;
        jumpForce = 30f;
        isJumping = false;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal2");
        moveVertical = Input.GetAxisRaw("Vertical2");

        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
        if (Input.GetKeyDown("n"))
        {
            Attack();
        }
        if (Input.GetKeyDown("m"))
        {
            Block();

        }

        void Block()
        {
            animate.SetTrigger("Block");
            StartCoroutine("IFrames");
        }
        void Attack()
        {
            animate.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {


        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);

        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            animate.SetBool("Jump", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            animate.SetBool("Jump", false);
        }
        if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage(20);
        }
    }
    IEnumerator IFrames()
    {
        Physics2D.IgnoreLayerCollision(9, 11, true);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(9, 11, false);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }
    void OnBecameInvisible()
    {
        enabled = false;
        TakeDamage(100);
    }
    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    public void TakeDamage(int damage)
    {
        animate.SetTrigger("onHit");
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0 && !deathState)
        {
            Die();
        }
    }
    void Die()
    {
        deathState = true;
        Debug.Log("You have died!");
        animate.SetTrigger("Death");
        animate.SetBool("isDead", true);
        gameManager.victory();
        this.enabled = false;

    }

}
