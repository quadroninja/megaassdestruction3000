using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BulletBehavior
{
    [SerializeField] private LineRenderer lineRenderer;
    public float laserRange = 10f;

    void Update()
    {
        base.Update();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, base.direction, laserRange);
        lineRenderer.SetPosition(0, transform.position); // Начало лазера

        if (hit.collider != null)
        {
             // Конец лазера
            // Наносим урон врагу, если он есть
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                lineRenderer.SetPosition(1, hit.point);
                enemy.TakeDamage(base.damage);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, (Vector2)transform.position + base.direction * laserRange); // Конец лазера без столкновения
        }
    }
}
