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
}
