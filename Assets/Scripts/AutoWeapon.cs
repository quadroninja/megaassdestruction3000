using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject[] bulletPrefabs; // ������ �������� ����
    public float attackRange; // ��������� �����
    public float atkCooldown; // ����� ����������� ����� ����������

    private float nextAttackTime;
    private int currentBulletIndex = 0; // ������ ������� ����

    void Update()
    {
        // ��������, ����� �� ���������
        if (Time.time >= nextAttackTime)
        {
            // ����� ���������� �����
            GameObject closestEnemy = FindClosestEnemy();

            // ���� ���� ������ � ��������� �����
            if (closestEnemy != null)
            {
                // ������� ������ � �����
                transform.LookAt(closestEnemy.transform);

                // ������� �����
                FireBullet(closestEnemy.transform.position);

                // ���������� ������� ���������� ��������
                nextAttackTime = Time.time + atkCooldown;

                // ������� � ���������� ������� ���� � �������
                currentBulletIndex = (currentBulletIndex + 1) % bulletPrefabs.Length;
            }
        }
    }

    // ����� ���������� �����
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = attackRange; // ��������� ��������

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
        // �������� ����
        GameObject bullet = Instantiate(bulletPrefabs[currentBulletIndex], transform.position, Quaternion.identity);

        // ����������� ����
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // ���������� ����
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bullet.GetComponent<BulletBehavior>().speed);
    }
}
