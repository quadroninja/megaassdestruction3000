using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public float tickTime;
    private float timeCheck;
    public LayerMask whatIsSolid;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Damage");
            other.gameObject.GetComponent<HealthChanger>().TakeDamage(damage);
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
        transform.Translate(Vector2.up*speed*Time.deltaTime);
    }
}
