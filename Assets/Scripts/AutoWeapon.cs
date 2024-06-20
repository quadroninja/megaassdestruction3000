using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // Массив префабов пули
    public float attackRange; // Дальность атаки
    public float atkCooldown; // Время перезарядки между выстрелами

    private float nextAttackTime;
    private int currentBulletIndex = 0; // Индекс текущей пули

    void Update()
    {
        // Проверка, можно ли атаковать
        if (Time.time >= nextAttackTime)
        {
            // Поиск ближайшего врага
            GameObject closestEnemy = FindClosestEnemy();

            // Если враг найден в диапазоне атаки
            if (closestEnemy != null)
            {
                // Поворот оружия к врагу
                transform.LookAt(closestEnemy.transform);

                // Выстрел пулей
                FireBullet(closestEnemy.transform.position);

                // Обновление времени следующего выстрела
                nextAttackTime = Time.time + atkCooldown;

                // Переход к следующему префабу пули в массиве
                currentBulletIndex = (currentBulletIndex + 1) % bulletPrefabs.Length;
            }
        }
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
        GameObject bullet = Instantiate(bulletPrefabs[currentBulletIndex], transform.position, Quaternion.identity);

        // Направление пули
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Применение силы
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bullet.GetComponent<BulletBehavior>().speed);
    }
}
