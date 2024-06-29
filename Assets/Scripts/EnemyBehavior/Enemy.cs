using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    private Transform player;
    public float movespeed;
    [SerializeField] GameObject xpPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthChanger>().TakeDamage(damage);
        }
    }
    public void Update()
    {
        if (health <= 0)
        {
            Instantiate(xpPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //xpPrefab = GameObject.FindGameObjectWithTag("xp");
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
