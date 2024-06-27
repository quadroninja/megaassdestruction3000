using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Swap : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // Параметры способности
    public float cooldownTime = 5f;
    public float swapRadius = 5f;
    public float angerSpeed = 1f;
    public float preparation = 0f; //от 0 до 1

    private SpriteRenderer sprite;
    private Vector3 swapBuffer;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(AttackCoroutine());
    }

    bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) <= swapRadius;
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (IsPlayerInRange())
            {
                preparation += Time.deltaTime * angerSpeed;
                yield return null;
            }
            else
            {
                preparation = Math.Max(0, preparation - Time.deltaTime * angerSpeed);
                yield return null;
            }

            if (preparation >= 1 && IsPlayerInRange())
            {
                ApplySwapForce();
                preparation = 0;
                yield return new WaitForSeconds(cooldownTime);
            }
        }
    }

    private void ApplySwapForce()
    {
        
        if (IsPlayerInRange())
        {
            swapBuffer = player.transform.position;
            player.transform.position = transform.position;
            transform.position = swapBuffer;
        }
    }

    void Update()
    {
        sprite.color = new Color(1 - preparation, sprite.color.g, sprite.color.b, preparation);
    }
}
