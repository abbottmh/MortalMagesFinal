using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animate;
    public GameObject player;
    private float distance;
    public float speed;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    private bool facingRight = false;
    private bool deathState;
    public GameManager gameManager;



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
        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);


    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage(20);
        }
    }
    public void TakeDamage(int damage)
    {
        animate.SetTrigger("isHit");
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
        gameManager.victory();
        animate.SetBool("isDead", true);
        Debug.Log("Enemy has died!");
        this.enabled = false;
    }
    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

}
