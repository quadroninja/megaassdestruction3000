using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public WeaponInventory inventory;
    public float attackRange;



    void Update()
    {
        foreach(GameObject weapon in inventory.Weapons)
        {
            if (weapon != null)
            {
                WeaponBehaviour currentWeapon = weapon.GetComponent<WeaponBehaviour>();
                if (currentWeapon.canShoot())
                {
                    // Поиск ближайшего врага
                    GameObject closestEnemy = FindClosestEnemy();

                    if (closestEnemy != null)
                    {
                        Debug.Log("pow");
                        // Поворот оружия к врагу
                        //currentWeapon.transform.LookAt(closestEnemy.transform.position);
                        currentWeapon.transform.up = closestEnemy.transform.position - this.transform.position;
                        currentWeapon.fire();
                    }
                }
            }
        }
        foreach (GameObject weapon in inventory.Extra)
        {
            if (weapon != null)
            {
                WeaponBehaviour currentWeapon = weapon.GetComponent<WeaponBehaviour>();
                if (currentWeapon.canShoot())
                {
                    // Поиск ближайшего врага
                    GameObject closestEnemy = FindClosestEnemy();

                    if (closestEnemy != null)
                    {
                        Debug.Log("pow");
                        // Поворот оружия к врагу
                        //currentWeapon.transform.LookAt(closestEnemy.transform.position);
                        currentWeapon.transform.up = closestEnemy.transform.position - this.transform.position -  new Vector3(1f, 1f,0f);
                        currentWeapon.fire();
                        currentWeapon.transform.up = closestEnemy.transform.position - this.transform.position - new Vector3(-1f, -1f, 0f);
                        currentWeapon.fire();
                    }
                }
            }
        }
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
}
