using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float baseMoveSpeed;
    public float moveSpeed;
    public Rigidbody2D rb;
    private float timeCheck;
    public float maxStunTime;
    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        if (moveSpeed != 0)
        {
            baseMoveSpeed = moveSpeed;
        }
        else timeCheck -= Time.deltaTime;
        if (timeCheck < 0)
        {
            moveSpeed = baseMoveSpeed;
            timeCheck = maxStunTime;
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
}
