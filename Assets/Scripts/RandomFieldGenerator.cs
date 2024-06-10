using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldGenerator : MonoBehaviour
{
    // Клетки для генерации
    public GameObject[] cellPrefabs;
    public GameObject bonusPrefab;
    // Тайлмап
    public Tilemap tilemap;

    // Персонаж
    public Transform player;

    // Вероятность появления бонусов
    private const float bonusChance = 0.005f;

    // Метод для генерации поля
    public void GenerateField()
    {
        // Получаем координаты игрока
        int playerX = Mathf.RoundToInt(player.position.x);
        int playerY = Mathf.RoundToInt(player.position.y);

        // Проверка условия остановки генерации
        if (playerX + playerY > 10000)
        {
            return;
        }

        // Генерация клеток вокруг персонажа
        for (int x = playerX; x <= playerX + 100; x++)
        {
            for (int y = playerY; y <= playerY + 100; y++)
            {
                // Проверка, что клетка не сгенерирована
                if (tilemap.GetTile(new Vector3Int(x, y, 0)) == null)
                {
                    // Выбираем случайный тип клетки
                    int randomIndex = Random.Range(0, cellPrefabs.Length);
                    GameObject cell = Instantiate(cellPrefabs[randomIndex], new Vector3(x, y, 0), Quaternion.identity);

                    // Добавляем клетку на тайлмап
                    tilemap.SetTile(new Vector3Int(x, y, 0), cell.GetComponent<Tile>());

                    // Генерация бонуса с заданной вероятностью
                    if (Random.value < bonusChance)
                    {
                        GameObject bonus = Instantiate(bonusPrefab, cell.transform.position, Quaternion.identity);
                        bonus.transform.SetParent(cell.transform);
                        // ... (Логика добавления бонуса на клетку)
                    }
                }
            }
        }
    }

    // Вызываем метод генерации поля в Update
    void Update()
    {
        GenerateField();
    }
}