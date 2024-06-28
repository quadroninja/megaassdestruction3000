using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BulletBehavior
{
    [SerializeField] private LineRenderer lineRenderer;
    public float laserRange = 10f;
    public float laserCooldown;
    private float timer;
    private bool follow = false;
    private float followRad = 7f;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            fire();
        }
        followers();
        lineRenderer.SetPosition(0, transform.position); // Начало лазера
    }

    void followers() 
    {
        if(follow && Vector2.Distance(transform.position, player.transform.position) > followRad)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, base.speed);
        }
        else if(follow && Vector2.Distance(transform.position, player.transform.position) < followRad)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -base.speed);
        }
    }

    void fire()
    {
        GameObject e = FindClosestEnemy();
        if (e != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, e.transform.position, laserRange);
            lineRenderer.SetPosition(0, transform.position);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Конец лазера
                    // Наносим урон врагу, если он есть
                    if (hit.collider.TryGetComponent(out Enemy enemy))
                    {
                        lineRenderer.SetPosition(1, hit.point);
                        enemy.TakeDamage(base.damage);
                    }
                }
            }
            timer = laserCooldown;
        }
    }
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = laserRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
    public void change(bool i)
    {
        follow = i;
    }
}
