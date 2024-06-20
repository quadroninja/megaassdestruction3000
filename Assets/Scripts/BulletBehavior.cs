using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public float atkSpeed;
    public float cooldown;
    public int damage;
    public float tickTime;
    private float timeCheck;
    public LayerMask whatIsSolid;

    void Start()
    {
        cooldown = atkSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && timeCheck < 0)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            timeCheck = tickTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timeCheck -= Time.deltaTime;
        if (lifetime<0)
        {
            Destroy(gameObject);
        }
        else lifetime -= Time.deltaTime;
        transform.Translate(Vector2.up*speed*Time.deltaTime);
    }
    public void reload()
    {
        cooldown = atkSpeed;
    }
}
