using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // ������ �������� ����
    private int weaponsAvilable = 0;
    public float attackRange;
    private int bulletIndex = 0;

    void Update()
    {
        foreach(var bullet in bulletPrefabs)
        {
            if (bullet != null)
            {
                BulletBehavior weapon = bullet.GetComponent<BulletBehavior>();
                if (weapon.cooldown <= 0)
                {
                    // ����� ���������� �����
                    GameObject closestEnemy = FindClosestEnemy();

                    if (closestEnemy != null)
                    {
                        // ������� ������ � �����
                        transform.LookAt(closestEnemy.transform);

                        FireBullet(closestEnemy.transform.position);

                        weapon.reload();
                    }
                }
                else weapon.cooldown -= Time.deltaTime;
            }
            bulletIndex++;
        }
        bulletIndex = 0;
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = attackRange;

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

    // ������� �����
    void FireBullet(Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefabs[bulletIndex], transform.position, Quaternion.identity);

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        bullet.transform.up = direction; // ���� ���� ������ ��������� ����
    }

    public void addWeapon(GameObject weapon)
    {
        if (weaponsAvilable < bulletPrefabs.Length)
        {
            bulletPrefabs[weaponsAvilable] = weapon;
            weaponsAvilable++;
            Debug.Log("Added weapon: " + weapon.name);
        }
    }
}
