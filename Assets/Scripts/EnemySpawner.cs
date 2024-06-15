using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy;
    public int enemyCount = 10;
    public float minSpawnRadius = 5f;
    public float maxSpawnRadius = 10f;
    public float spawnDelay = 5f;

    private GameObject player;
    public float[] initialSpawnProbabilities = { 0.75f, 0.20f, 0.04f, 0.01f, 0.00f };
    public float[] finalSpawnProbabilities = { 0.05f, 0.10f, 0.20f, 0.30f, 0.35f };

    private int currentWave = 0;
    private void Start()
    {
        // Найти игрока по тэгу 
        player = GameObject.FindGameObjectWithTag("Player");

        // Запустить первый спавн 
        InvokeRepeating("SpawnEnemies", 0f, spawnDelay);
    }
    void SpawnEnemies()
    {
        // ... (определяем `enemyCount` и `currentEnemyLevel`)

        // Вычисляем текущие вероятности спавна для текущей волны
        float[] currentProbabilities = CalculateProbabilities(currentWave);

        // Спавним врагов
        for (int i = 0; i < enemyCount; i++)
        {
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            float angle = Random.Range(0f, 360f);
            Vector2 spawnPosition = new Vector2(
                player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                player.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            int enemyIndex = ChooseEnemyIndex(currentProbabilities);

            // Создаем врага
            Instantiate(enemy[enemyIndex], spawnPosition, Quaternion.identity);
        }

        currentWave++; // Увеличиваем номер волны
    }

    // Метод для вычисления текущих вероятностей спавна для заданной волны
    private float[] CalculateProbabilities(int wave)
    {
        float[] probabilities = new float[enemy.Length];

        // Линейная интерполяция между начальными и конечными вероятностями
        for (int i = 0; i < probabilities.Length; i++)
        {
            probabilities[i] = Mathf.Lerp(initialSpawnProbabilities[i], finalSpawnProbabilities[i], wave / 19f);
        }

        return probabilities;
    }

    // Метод для случайного выбора индекса врага с учетом вероятностей
    private int ChooseEnemyIndex(float[] probabilities)
    {
        float randomValue = Random.value;
        float cumulativeProbability = 0f;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                return i;
            }
        }

        // В случае ошибки (что маловероятно) возвращаем последний индекс
        return probabilities.Length - 1;
    }
}