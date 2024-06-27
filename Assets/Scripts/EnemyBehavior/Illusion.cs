using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illusion : Enemy
{
    public float lifetime;
    private float timer;
    void Start()
    {
        timer = lifetime;
    }
    void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
