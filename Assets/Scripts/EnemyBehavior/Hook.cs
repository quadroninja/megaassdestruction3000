using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public float pullForce;
    public float tickTime;
    private float timeCheck;
    private GameObject player= null;
    public LayerMask whatIsSolid;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && timeCheck < 0)
        {
            player = other.gameObject;
            player.GetComponent<PlayerMovement>().getHooked(this.gameObject);
            //gameObject.transform.up = new Vector2(Vector2.up.x-180f, Vector2.up.y-180f);
            timeCheck = tickTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timeCheck -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
        else lifetime -= Time.deltaTime;
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    void OnDestroy()
    {
        if (player != null && player.transform.IsChildOf(this.transform))
        {
            player.GetComponent<PlayerMovement>().getUnhooked();
            foreach (Behaviour comp in player.GetComponents<Behaviour>())
                comp.enabled = true;
        }
        //player.transform.position = gameObject.transform.position;    
    }
}
