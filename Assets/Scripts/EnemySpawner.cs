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
        // ����� ������ �� ���� 
        player = GameObject.FindGameObjectWithTag("Player");

        // ��������� ������ ����� 
        InvokeRepeating("SpawnEnemies", 0f, spawnDelay);
    }
    void SpawnEnemies()
    {
        // ... (���������� `enemyCount` � `currentEnemyLevel`)

        // ��������� ������� ����������� ������ ��� ������� �����
        float[] currentProbabilities = CalculateProbabilities(currentWave);

        // ������� ������
        for (int i = 0; i < enemyCount; i++)
        {
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            float angle = Random.Range(0f, 360f);
            Vector2 spawnPosition = new Vector2(
                player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                player.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            int enemyIndex = ChooseEnemyIndex(currentProbabilities);

            // ������� �����
            Instantiate(enemy[enemyIndex], spawnPosition, Quaternion.identity);
        }

        currentWave++; // ����������� ����� �����
    }

    // ����� ��� ���������� ������� ������������ ������ ��� �������� �����
    private float[] CalculateProbabilities(int wave)
    {
        float[] probabilities = new float[enemy.Length];

        // �������� ������������ ����� ���������� � ��������� �������������
        for (int i = 0; i < probabilities.Length; i++)
        {
            probabilities[i] = Mathf.Lerp(initialSpawnProbabilities[i], finalSpawnProbabilities[i], wave / 19f);
        }

        return probabilities;
    }

    // ����� ��� ���������� ������ ������� ����� � ������ ������������
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

        // � ������ ������ (��� ������������) ���������� ��������� ������
        return probabilities.Length - 1;
    }
}