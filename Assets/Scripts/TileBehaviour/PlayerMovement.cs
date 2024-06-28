using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float baseMoveSpeed;
    public float moveSpeed;
    public Rigidbody2D rb;
    private float timeCheck;
    private float waitTime;
    private Vector2 moveDirection;
    private bool isHooked;
    public bool IsHooked { get { return isHooked; } }
    public float hookStrengthMultiplier = 20f;
    public float hookStrengthThreshold = 100f;
    public float hookStrengthDecreaseOnTap = 10f;
    private float hookStrengthCurrent = 0f;

    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void getHooked()
    {
        isHooked = true;
        hookStrengthCurrent = hookStrengthThreshold;
    }
    public void getUnhooked()
    {
        transform.parent = null;
        isHooked = false;
        hookStrengthCurrent = 0f;
    }
    void ProcessInputs()
    {
        if (isHooked)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                hookStrengthCurrent -= hookStrengthDecreaseOnTap;
            if (hookStrengthCurrent <= 0f)
                getUnhooked();
            hookStrengthCurrent = Mathf.Min(hookStrengthThreshold, hookStrengthCurrent + Time.deltaTime * hookStrengthMultiplier);
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x* moveSpeed, moveDirection.y*moveSpeed);
    }
}
