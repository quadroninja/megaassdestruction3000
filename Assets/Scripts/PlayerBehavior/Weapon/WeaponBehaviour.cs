using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed;
    public float lifetime;
    public float atkSpeed;
    public int damage;

    public float baseSpeed;
    public float baseLifetime;
    public float baseAtkSpeed;
    public int baseDamage;
    public float cooldown;
    
    public int level = 1;
    public bool upgradable;

    public float tickTime;
    private float timeCheck;
    public LayerMask whatIsSolid;
    public float speedScale;
    public float lifetimeScale;
    public double damageScale;
    public float atkSpeedScale;

    void Start()
    {
        speed = baseSpeed;
        lifetime = baseLifetime;
        atkSpeed = baseAtkSpeed;
        damage = baseDamage;
        cooldown = atkSpeed;
    }
    public void fire()
    {
        gameObject.GetComponent<AudioSource>().Play();
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<BulletBehavior>().Init(speed, lifetime, damage, transform.up);
        cooldown = atkSpeed;
    }

    public bool canShoot()
    {
        return cooldown < 0;
    }

    public void newlevel()
    {
        level++;
        weaponScalings();
        upgradable = true;
    }

    public void weaponScalings()
    {
        damage = (int)(baseDamage + (baseDamage * damageScale * (level-1)));
        speed= baseSpeed + (baseSpeed * speedScale * (level-1));
        lifetime = baseLifetime + (baseLifetime * lifetimeScale * (level-1));
        atkSpeed = baseAtkSpeed - (baseAtkSpeed * atkSpeedScale * (level-1));
        Debug.Log("Weapon scaled");
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && timeCheck < 0)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            timeCheck = tickTime;
        }
    }
    */
    // Update is called once per frame
    public void Update()
    {
        cooldown -= Time.deltaTime;
    }
}
