using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illusion : Enemy
{
    public float lifetime;
    private float timer;
    void Start()
    {
        base.Start();
        timer = lifetime;
    }
    void FixedUpdate()
    {
        base.Update();
        base.FixedUpdate();
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
