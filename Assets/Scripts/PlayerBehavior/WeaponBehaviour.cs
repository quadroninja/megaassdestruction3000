using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float speed;
    public float lifetime;
    public float distance;
    public float atkSpeed;
    public float cooldown;
    public int damage;
    public int level = 1;
    public float tickTime;
    private float timeCheck;
    public LayerMask whatIsSolid;

    void Start()
    {
        cooldown = atkSpeed;
    }
    public void fire()
    {
        Debug.Log(transform.up);
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
        //weaponScalings();
    }

    /*public void weaponScalings(){
      
        damage=
        speed=
        liftime=
        ט עה
    }    */
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
    void Update()
    {
        cooldown -= Time.deltaTime;
    }
}
