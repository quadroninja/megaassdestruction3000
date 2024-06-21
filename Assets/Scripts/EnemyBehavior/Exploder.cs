using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemy
{
    // Создание экземпляра способности
    public Explode explode;

    void Start()
    {
        // Инициализация способности
        explode = GetComponent<Explode>();
    }

    void Update()
    {
        // Вызов метода Update() базового класса Enemy
        base.Update();
        // Обновление состояния способности
        explode.Update();
        if (base.health < 0)
        {
            explode.ActivateExplodeAbility();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
}
