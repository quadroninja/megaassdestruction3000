using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker : Enemy
{
    // Создание экземпляра способности
    public HookShot hookShot;

    void Start()
    {
        // Инициализация способности
        hookShot = GetComponent<HookShot>();
    }
}
