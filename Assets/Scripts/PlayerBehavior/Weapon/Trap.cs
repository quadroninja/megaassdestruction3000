using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : BulletBehavior
{
    public bool explodable= false;
    public float cooldown;
    private float timer;
    public float damageDelay = 2f;
    public GameObject explosion;


    private IEnumerator dealDamage(GameObject toDeal)
    {
        while (toDeal != null) 
        {
            toDeal.GetComponent<Enemy>().TakeDamage(damage);
            yield return new WaitForSeconds(damageDelay);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(dealDamage(other.gameObject));
            //other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        StopCoroutine(dealDamage(other.gameObject));
    }

    void Update()
    {
        timer -= Time.deltaTime;
        base.Update();
        if (explodable && timer<0)
        {
            explode();
        }
    }
    public void change(bool i)
    {
        explodable = i;
    }
    public void explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        timer = cooldown;
    }
}
