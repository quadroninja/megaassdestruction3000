using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : BulletBehavior
{
    public bool explodable= false;
    public float cooldown;
    private float timer;
    public GameObject explosion;

    void Update()
    {
        timer -= Time.deltaTime;
        base.Update();
        if (explodable && timer<0)
        {
            explode();
        }
    }
    public void change(bool i)
    {
        explodable = i;
    }
    public void explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        timer = cooldown;
    }
}
