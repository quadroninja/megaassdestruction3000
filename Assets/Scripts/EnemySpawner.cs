using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Настройки спавна 
    public List<GameObject> enemy; // Префаб врага 
    public int enemyCount = 10; // Количество врагов в волне 
    public float minSpawnRadius = 5f; // Минимальный радиус спавна от игрока 
    public float maxSpawnRadius = 10f; // Максимальный радиус спавна от игрока 
    public float spawnDelay = 5; // Время между волнами 
    public float timeRemaining;

    private GameObject player; // Игрок 

    private void Start()
    {
        // Найти игрока по тэгу 
        player = GameObject.FindGameObjectWithTag("Player");

        timeRemaining = spawnDelay;
        // Запустить первый спавн 
        InvokeRepeating("SpawnEnemies", 0f, spawnDelay);
    }

    void FixedUpdate()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        if (timeRemaining<0)
        {
            SpawnEnemies();
            timeRemaining = spawnDelay;
        }
    }
    // Функция спавна врагов 
    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int type = Random.Range(0, enemy.Count);
            // Генерация случайного радиуса и угла 
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            float angle = Random.Range(0f, 360f);

            // Вычисление координат спавна 
            Vector2 spawnPosition = new Vector2(
                player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                player.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            // Создание врага 
            Instantiate(enemy[type], spawnPosition, Quaternion.identity);
        }
    }
}