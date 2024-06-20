using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // Массив префабов пули
    public float attackRange; // Дальность атаки
    private int bulletIndex = 0;

    void Update()
    {
        foreach(var bullet in bulletPrefabs)
        {
            BulletBehavior weapon = bullet.GetComponent<BulletBehavior>();
            if (weapon.cooldown <= 0)
            {
                // Поиск ближайшего врага
                GameObject closestEnemy = FindClosestEnemy();

                // Если враг найден в диапазоне атаки
                if (closestEnemy != null)
                {
                    // Поворот оружия к врагу
                    transform.LookAt(closestEnemy.transform);

                    FireBullet(closestEnemy.transform.position);

                    weapon.reload();
                }
            }
            else weapon.cooldown -= Time.deltaTime;
            bulletIndex++;
        }
        bulletIndex = 0;
    }

    // Поиск ближайшего врага
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = attackRange; // Начальное значение

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    // Выстрел пулей
    void FireBullet(Vector2 targetPosition)
    {
        // Создание пули
        GameObject bullet = Instantiate(bulletPrefabs[bulletIndex], transform.position, Quaternion.identity);

        // Направление пули
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Применение силы
        bullet.transform.up = direction; // Поворачивает пулю в направлении "верх"
    }
}
