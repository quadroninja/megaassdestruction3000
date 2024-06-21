using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : Enemy
{
    // Создание экземпляра способности
    public Swap swap;

    void Start()
    {
        // Инициализация способности
        swap = GetComponent<Swap>();
    }

    void Update()
    {
        // Вызов метода Update() базового класса Enemy
        base.Update();
        // Обновление состояния способности
        swap.Update();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
}
