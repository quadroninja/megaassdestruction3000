using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // ��������� ������ 
    public List<GameObject> enemy; // ������ ����� 
    public int enemyCount = 10; // ���������� ������ � ����� 
    public float minSpawnRadius = 5f; // ����������� ������ ������ �� ������ 
    public float maxSpawnRadius = 10f; // ������������ ������ ������ �� ������ 
    public float spawnDelay = 5f; // ����� ����� ������� 

    private GameObject player; // ����� 

    private void Start()
    {
        // ����� ������ �� ���� 
        player = GameObject.FindGameObjectWithTag("Player");

        // ��������� ������ ����� 
        InvokeRepeating("SpawnEnemies", 0f, spawnDelay);
    }

    // ������� ������ ������ 
    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int type = Random.Range(0, enemy.Count);
            // ��������� ���������� ������� � ���� 
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            float angle = Random.Range(0f, 360f);

            // ���������� ��������� ������ 
            Vector2 spawnPosition = new Vector2(
                player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                player.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            // �������� ����� 
            Instantiate(enemy[type], spawnPosition, Quaternion.identity);
        }
    }
}