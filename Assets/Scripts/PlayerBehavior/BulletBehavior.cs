using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;
    public Vector2 direction;

    public void Init(float spd, float lt, int dam, Vector2 dir)
    {
        speed = spd;
        lifetime = lt;
        damage = dam;
        direction = dir;
        //this.transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (lifetime<0)
        {
            Destroy(gameObject);
        }
        else lifetime -= Time.deltaTime;

        //transform.LookAt(direction);
        transform.Translate(direction*speed*Time.deltaTime);
    }
}
