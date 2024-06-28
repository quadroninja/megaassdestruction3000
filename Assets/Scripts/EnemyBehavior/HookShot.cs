using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HookShot : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // Параметры способности
    public float cooldownTime = 5f;
    public float swapRadius = 5f;
    public float angerSpeed = 1f;
    public float preparation = 0f; //от 0 до 1
    private SpriteRenderer sprite;
    public GameObject hook;

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
                ApplyPullForce();
                preparation = 0;
                yield return new WaitForSeconds(cooldownTime);
            }
        }
    }
    void Update()
    {
        //transform.LookAt(player.transform);
        sprite.color = new Color(1 - preparation, sprite.color.g, sprite.color.b);
    }

    private void ApplyPullForce()
    {
        GameObject spw = Instantiate(hook, transform.position, transform.rotation, transform);
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        spw.transform.up = direction;
    }
}
