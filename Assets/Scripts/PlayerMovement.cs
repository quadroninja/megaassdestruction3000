using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.ChangeMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x* moveSpeed, moveDirection.y*moveSpeed);
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.ChangeHealth(currentHealth);
    }
}
