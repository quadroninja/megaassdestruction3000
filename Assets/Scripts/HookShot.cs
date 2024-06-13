using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // Параметры способности
    public float cooldownTime = 5f;
    public float pullRadius = 5f;
    public float pullForce = 10f;
    private float buffer = 0;
    // Время действия силы притяжения
    public float pullDuration = 1f;

    // Время до следующей активации
    private float nextActivationTime;
    // Время окончания действия силы притяжения
    private float pullEndTime;

    public void ActivateAbility()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        if (Vector2.Distance(transform.position, player.transform.position) <= pullRadius)
        {
            buffer = playerMovement.moveSpeed;
            playerMovement.moveSpeed = 0;
            // Запускаем притяжение
            pullEndTime = Time.time + pullDuration;

            // Применяем силу притяжения с интервалом
            while (pullEndTime >= 0)
            {
                ApplyPullForce();
                pullEndTime -= Time.deltaTime;
            }
            pullEndTime = pullDuration;
            playerMovement.moveSpeed = buffer;
        }
    }
    public void DeactivateAbility()
    {
        playerMovement.moveSpeed = buffer;
    }
        // Метод для применения силы притяжения с интервалом
    private void ApplyPullForce()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) <= pullRadius && Time.time < pullEndTime)
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;
            player.GetComponent<Rigidbody2D>().AddForce(direction * pullForce);
        }
    }

    // Обновление состояния
    public void Update()
    {
        // Проверка времени для активации способности
        if (nextActivationTime < 0)
        {
            ActivateAbility();
            nextActivationTime = cooldownTime;
        }
        else nextActivationTime -= Time.deltaTime;
    }
}
