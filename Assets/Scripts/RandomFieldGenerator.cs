using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldGenerator : MonoBehaviour
{
    // ������ ��� ���������
    public GameObject[] cellPrefabs;
    public GameObject bonusPrefab;
    // �������
    public Tilemap tilemap;

    // ��������
    public Transform player;

    // ����������� ��������� �������
    private const float bonusChance = 0.005f;

    // ����� ��� ��������� ����
    public void GenerateField()
    {
        // �������� ���������� ������
        int playerX = Mathf.RoundToInt(player.position.x);
        int playerY = Mathf.RoundToInt(player.position.y);

        // �������� ������� ��������� ���������
        if (playerX + playerY > 10000)
        {
            return;
        }

        // ��������� ������ ������ ���������
        for (int x = playerX; x <= playerX + 100; x++)
        {
            for (int y = playerY; y <= playerY + 100; y++)
            {
                // ��������, ��� ������ �� �������������
                if (tilemap.GetTile(new Vector3Int(x, y, 0)) == null)
                {
                    // �������� ��������� ��� ������
                    int randomIndex = Random.Range(0, cellPrefabs.Length);
                    GameObject cell = Instantiate(cellPrefabs[randomIndex], new Vector3(x, y, 0), Quaternion.identity);

                    // ��������� ������ �� �������
                    tilemap.SetTile(new Vector3Int(x, y, 0), cell.GetComponent<Tile>());

                    // ��������� ������ � �������� ������������
                    if (Random.value < bonusChance)
                    {
                        GameObject bonus = Instantiate(bonusPrefab, cell.transform.position, Quaternion.identity);
                        bonus.transform.SetParent(cell.transform);
                        // ... (������ ���������� ������ �� ������)
                    }
                }
            }
        }
    }

    // �������� ����� ��������� ���� � Update
    void Update()
    {
        GenerateField();
    }
}