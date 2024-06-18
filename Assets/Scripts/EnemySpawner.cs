using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Массив префабов врагов
    public Transform player; // Ссылка на игрока

    public float spawnRadiusMin = 2f; // Минимальный радиус спавна
    public float spawnRadiusMax = 5f; // Максимальный радиус спавна
    public float spawnIntervalMin = 0.5f; // Минимальный интервал спавна (в секундах)
    public float spawnIntervalMax = 2f; // Максимальный интервал спавна (в секундах)

    public float waveDuration = 30f; // Длительность волны (в секундах)
    public int maxEnemiesPerWave = 10; // Максимальное количество врагов за волну

    public TextMeshProUGUI waveText; // Используйте TextMeshProUGUI
    public TextMeshProUGUI timerText; // Используйте TextMeshProUGUI

    public int currentWave = 1; // Текущая волна
    private float waveStartTime; // Время начала волны
    private int enemiesSpawnedThisWave = 0; // Количество врагов, спавненных на текущей волне

    public int pointsPerWave = 100; // Очки для спавна врагов на волне
    private int currentPoints = 0;
    public int[] enemyPointCosts = { 10, 20, 30, 40, 50 }; // Стоимость каждого типа врага в очках

    // Вероятности появления врагов на каждой волне (можно заменить на более гибкий алгоритм)
    private float[][] enemyProbabilities = {
        new float[] { 0.75f, 0.20f, 0.04f, 0.01f, 0.00f }, // 1 волна
        new float[] { 0.65f, 0.25f, 0.06f, 0.02f, 0.02f }, // 2 волна
        new float[] { 0.55f, 0.30f, 0.10f, 0.10f, 0.00f },
        new float[] { 0.45f, 0.35f, 0.04f, 0.01f, 0.00f },
        new float[] { 0.35f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.65f, 0.35f, 0f, 0.00f },// первый тест, волна только с мелкими чтоб проверить скорость урона и умение убегать
        new float[] { 0.15f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0.05f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0.5f, 0f, 0f, 0.25f, 0.25f }, // тут учим игрока держаться на дистанции, чтобы не быть захуканым к толпе обычек + проверяем умение держаться на расстоянии
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0f, 0f, 0f, 0f },// добавить: врагов к которым нужно подходить, полная противоположность 11 волне
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f },
        new float[] { 0f, 0.20f, 0.04f, 0.01f, 0.00f }// добавить: босс, тут впринципе 1 заспавнил а дальше как в обычной волне
        // ... и так далее для каждой волны
    };

    // Масштабирование урона и здоровья врагов по волнам
    private int[] damageScale = { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
    private int[] healthScale = { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};

    public GameObject startWindow; // Префаб окна "Старт"
    public Button startButton; // Кнопка "Старт" в окне

    private bool waveStarted = false; // Флаг, указывающий, началась ли волна

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        UpdateWaveTimer();
    }

    // Запуск новой волны
    void StartWave()
    {
        waveStartTime = Time.time;
        enemiesSpawnedThisWave = 0;
        waveText.text = "Wave: " + currentWave;
        timerText.text = "Time: " + waveDuration;
        if (!waveStarted)
        {
            waveStarted = true;
            SpawnEnemiesContinuously(); // Запускаем спавн врагов
        }
    }

    // Обновление таймера волны
    void UpdateWaveTimer()
    {
        float timeRemaining = waveDuration - (Time.time - waveStartTime);
        timerText.text = "Time: " + Mathf.FloorToInt(timeRemaining);

        if (timeRemaining <= 0 && waveStarted==true)
        {
            NextWave();
        }
    }

    // Переход к следующей волне
    void NextWave()
    {
        waveStarted = false;
        StopWaveSpawning(); // Останавливаем спавн врагов
        currentWave++;
        if (currentWave > 20)
        {
            // Конец игры
            Debug.Log("Game Over");
            return;
        }
        // Показываем окно "Старт"
        startWindow.SetActive(true);
        startButton.onClick.AddListener(StartNextWave);
    }

    // Запуск следующей волны при нажатии на кнопку "Старт"
    void StartNextWave()
    {
        // Скрываем окно "Старт"
        startWindow.SetActive(false);
        startButton.onClick.RemoveAllListeners();
        StartWave(); // Запускаем следующую волну
    }

    void SpawnEnemyGroup()
    {
        // Определяем количество врагов для спавна
        int enemyCount = Random.Range(1, maxEnemiesPerWave + 1);
        currentPoints = pointsPerWave;
        // Вычисляем текущие вероятности спавна для текущей волны
        float[] currentProbabilities = enemyProbabilities[currentWave-1];

        // Спавним врагов
        for (int i = 0; i < enemyCount; i++)
        {
            // Генерируем случайную точку в радиусе вокруг игрока
            Vector2 spawnPosition = Random.insideUnitCircle * Random.Range(spawnRadiusMin, spawnRadiusMax);
            spawnPosition += (Vector2)player.position;

            // Выбираем случайного врага с учетом текущих вероятностей
            int enemyIndex = ChooseRandomEnemy(currentProbabilities, enemyPointCosts);

            // Создаем врага
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
        // Создаем список доступных врагов с учетом стоимости
        List<int> availableEnemies = new List<int>();
        for (int i = 0; i < probabilities.Length; i++)
        {
            if (currentPoints >= pointCosts[i])
            {
                availableEnemies.Add(i);
            }
        }

        // Если нет доступных врагов, возвращаем последний индекс (чтобы избежать ошибки)
        if (availableEnemies.Count == 0)
        {
            return 0;
        }

        // Выбираем случайного врага из списка доступных
        float[] cumulativeProbabilities = new float[availableEnemies.Count];
        cumulativeProbabilities[0] = probabilities[availableEnemies[0]];
        for (int i = 1; i < availableEnemies.Count; i++)
        {
            cumulativeProbabilities[i] = cumulativeProbabilities[i - 1] + probabilities[availableEnemies[i]];
        }

        // Выбираем случайного врага с учетом накопительной вероятности
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

    // Метод для запуска спавна врагов с определенным интервалом
    void SpawnEnemiesContinuously()
    {
        InvokeRepeating("SpawnEnemyGroup", Random.Range(spawnIntervalMin, spawnIntervalMax), Random.Range(spawnIntervalMin, spawnIntervalMax));
    }

    // Метод для остановки спавна врагов
    void StopEnemiesContinuously()
    {
        CancelInvoke("SpawnEnemy");
    }

    // Метод для запуска спавна врагов в начале волны
    void StartWaveSpawning()
    {
        SpawnEnemiesContinuously();
    }

    // Метод для остановки спавна врагов в конце волны
    void StopWaveSpawning()
    {
        StopEnemiesContinuously();
    }
}