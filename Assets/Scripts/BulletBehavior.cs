using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public float tickTime;
    private float timeCheck;
    public LayerMask whatIsSolid;

    // Update is called once per frame
    void Update()
    {
        timeCheck -= Time.deltaTime;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (lifetime<0)
        {
            Destroy(gameObject);
        }
        else lifetime -= Time.deltaTime;
        if (hitInfo.collider != null && timeCheck<0)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                if (gameObject.tag == "Destroyable")
                {
                    Destroy(gameObject);
                }
                timeCheck = tickTime;
            }
        }
        transform.Translate(Vector2.up*speed*Time.deltaTime);
    }
}