using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Параметры способности
    public float ExplosionRadius = 5f;
    public float minSpawnRadius = 1f;
    public float maxSpawnRadius = 5f;
    public int cnt = 0;
    public GameObject bigBadaBum;

    public void ActivateExplodeAbility()
    {
        Death();
    }
    private void Death()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) <= ExplosionRadius)
        {
            for (int i = 0; i < cnt; i++)
            {
                float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
                float angle = Random.Range(0f, 360f);
                Vector2 spawnPosition = new Vector2(
                    transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                    transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad)
                );
                Instantiate(bigBadaBum, spawnPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    // Обновление состояния
    public void Update()
    {
        ActivateExplodeAbility();
    }
}
