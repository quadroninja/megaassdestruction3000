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
    // Время до следующей активации
    private float nextActivationTime;

    public void ActivateAbility()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        if (Vector2.Distance(transform.position, player.transform.position) <= pullRadius)
        {
            buffer = playerMovement.moveSpeed;
            playerMovement.moveSpeed = 0;
            ApplyPullForce();
            playerMovement.moveSpeed = buffer;
        }
    }
    public void DeactivateAbility()
    {
        playerMovement.moveSpeed = buffer;
    }
    private void ApplyPullForce()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) <= pullRadius)
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
