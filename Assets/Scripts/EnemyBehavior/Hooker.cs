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

    void Update()
    {
        // Вызов метода Update() базового класса Enemy
        base.Update();
        // Обновление состояния способности
        hookShot.Update();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
}
