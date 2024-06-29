using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker : Enemy
{
    // Создание экземпляра способности
    public HookShot hookShot;

    void Start()
    {
        base.Start();
        // Инициализация способности
        hookShot = GetComponent<HookShot>();
    }
    void FixedUpdate()
    {
        base.Update();
        base.FixedUpdate();
    }
}
