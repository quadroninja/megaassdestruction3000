using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public Transform player;
    public float movespeed;

    public void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up);
        if (hitInfo.collider.CompareTag("Player"))
        {
            hitInfo.collider.GetComponent<HealthChanger>().TakeDamage(damage);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movespeed);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}