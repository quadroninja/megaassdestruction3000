using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeShapedAtack : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public float atkSpeed;
    public LayerMask whatIsSolid;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
        else lifetime -= Time.deltaTime;
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (gameObject.tag == "Destroyable")
            {
                Destroy(gameObject);
            }
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
