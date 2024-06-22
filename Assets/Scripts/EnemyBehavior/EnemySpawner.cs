using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ������ �������� ������
    public Transform player; // ������ �� ������

    public float spawnRadiusMin = 2f; // ����������� ������ ������
    public float spawnRadiusMax = 5f; // ������������ ������ ������
    public float spawnIntervalMin = 0.5f; // ����������� �������� ������ (� ��������)
    public float spawnIntervalMax = 2f; // ������������ �������� ������ (� ��������)

    public float[] waveDuration; // ������������ ����� (� ��������)
    public int[] minEnemiesPerWave;
    public int[] maxEnemiesPerWave; // ������������ ���������� ������ �� �����

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timerText;

    public int currentWave = 1; // ������� �����
    private float waveStartTime; // ����� ������ �����
    private int enemiesSpawnedThisWave = 0; // ���������� ������, ������������ �� ������� �����

    public int[] pointsPerWave; // ���� ��� ������ ������ �� �����
    private int currentPoints = 0;
    public int[] enemyPointCosts = { 10, 20, 30, 40, 50 }; // ��������� ������� ���� ����� � �����

    // ����������� ��������� ������ �� ������ �����
    public float[][] enemyProbabilities = {
        new float[] { 0.75f, 0.20f, 0.04f, 0.01f, 0.00f }, // 1 �����
        new float[] { 0.65f, 0.25f, 0.06f, 0.02f, 0.02f }, // 2 �����
        new float[] { 0.55f, 0.30f, 0.10f, 0.10f, 0.00f },
        new float[] { 0.45f, 0.35f, 0.04f, 0.01f, 0.00f },
        new float[] { 0.35f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.65f, 0.35f, 0f, 0.00f },// ������ ����, ����� ������ � ������� ���� ��������� �������� ����� � ������ �������
        new float[] { 0.15f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0.05f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0.5f, 0f, 0f, 0.25f, 0.25f }, // ��� ���� ������ ��������� �� ���������, ����� �� ���� ��������� � ����� ������ + ��������� ������ ��������� �� ����������
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0f, 0f, 0f, 0f },// ��������: ������ � ������� ����� ���������, ������ ����������������� 11 �����
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },// ��������: ����, ��� ��������� 1 ��������� � ������ ��� � ������� �����
        // ... � ��� ����� ��� ������ �����
    };

    // ��������������� ����� � �������� ������ �� ������
    private int[] damageScale = { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
    private int[] healthScale = { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};

    public GameObject startWindow; // ������ ���� "�����"
    public Button startButton; // ������ "�����" � ����

    private bool waveStarted = false; // ����, �����������, �������� �� �����

    void Start()
    {
        startWindow.SetActive(false);
        StartWave();
    }

    void Update()
    {
        UpdateWaveTimer();
    }

    // ������ ����� �����
    void StartWave()
    {
        waveStartTime = Time.time;
        enemiesSpawnedThisWave = 0;
        waveText.text = "Wave: " + currentWave;
        timerText.text = "Time: " + waveDuration;
        if (!waveStarted)
        {
            waveStarted = true;
            SpawnEnemiesContinuously(); // ��������� ����� ������
        }
    }

    // ���������� ������� �����
    void UpdateWaveTimer()
    {
        float timeRemaining = waveDuration[currentWave - 1] - (Time.time - waveStartTime);
        timerText.text = "Time: " + Mathf.FloorToInt(timeRemaining);

        if (timeRemaining <= 0 && waveStarted==true)
        {
            NextWave();
        }
    }

    // ������� � ��������� �����
    void NextWave()
    {
        waveStarted = false;
        StopWaveSpawning(); // ������������� ����� ������
        currentWave++;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        if (currentWave > 20)
        {
            // ����� ����
            Debug.Log("Game Over");
            return;
        }
        // ���������� ���� "�����"
        startWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    // ������ ��������� ����� ��� ������� �� ������ "�����"
    public void StartNextWave()
    {
        // �������� ���� "�����"
        startWindow.SetActive(false);
        Time.timeScale = 1f;
        StartWave(); // ��������� ��������� �����
    }

    void SpawnEnemyGroup()
    {
        // ���������� ���������� ������ ��� ������
        int enemyCount = Random.Range(minEnemiesPerWave[currentWave-1], maxEnemiesPerWave[currentWave-1] + 1);
        currentPoints = pointsPerWave[currentWave-1];
        // ��������� ������� ����������� ������ ��� ������� �����
        float[] currentProbabilities = enemyProbabilities[currentWave-1];

        // ������� ������
        for (int i = 0; i < enemyCount; i++)
        {
            // ���������� ��������� ����� � ������� ������ ������
            Vector2 spawnPosition = Random.insideUnitCircle * Random.Range(spawnRadiusMin, spawnRadiusMax);
            spawnPosition += (Vector2)player.position;

            // �������� ���������� ����� � ������ ������� ������������
            int enemyIndex = ChooseRandomEnemy(currentProbabilities, enemyPointCosts);

            // ������� �����
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.damage = enemyScript.damage * damageScale[currentWave - 1];
                enemyScript.health = enemyScript.health * healthScale[currentWave - 1];
            }
            enemiesSpawnedThisWave++;
        }
    }

    int ChooseRandomEnemy(float[] probabilities, int[] pointCosts)
    {
        // ������� ������ ��������� ������ � ������ ���������
        List<int> availableEnemies = new List<int>();
        for (int i = 0; i < probabilities.Length; i++)
        {
            if (currentPoints >= pointCosts[i])
            {
                availableEnemies.Add(i);
            }
        }

        // ���� ��� ��������� ������, ���������� ��������� ������ (����� �������� ������)
        if (availableEnemies.Count == 0)
        {
            return 0;
        }

        // �������� ���������� ����� �� ������ ���������
        float[] cumulativeProbabilities = new float[availableEnemies.Count];
        cumulativeProbabilities[0] = probabilities[availableEnemies[0]];
        for (int i = 1; i < availableEnemies.Count; i++)
        {
            cumulativeProbabilities[i] = cumulativeProbabilities[i - 1] + probabilities[availableEnemies[i]];
        }

        // �������� ���������� ����� � ������ ������������� �����������
        float randomValue = Random.value;
        for (int i = 0; i < cumulativeProbabilities.Length; i++)
        {
            if (randomValue <= cumulativeProbabilities[i])
            {
                return availableEnemies[i];
            }
        }
        return availableEnemies[availableEnemies.Count - 1];
    }

    // ����� ��� ������� ������ ������ � ������������ ����������
    void SpawnEnemiesContinuously()
    {
        InvokeRepeating("SpawnEnemyGroup", Random.Range(spawnIntervalMin, spawnIntervalMax), Random.Range(spawnIntervalMin, spawnIntervalMax));
    }

    // ����� ��� ��������� ������ ������
    void StopEnemiesContinuously()
    {
        CancelInvoke("SpawnEnemy");
    }

    // ����� ��� ������� ������ ������ � ������ �����
    void StartWaveSpawning()
    {
        SpawnEnemiesContinuously();
    }

    // ����� ��� ��������� ������ ������ � ����� �����
    void StopWaveSpawning()
    {
        StopEnemiesContinuously();
    }
}