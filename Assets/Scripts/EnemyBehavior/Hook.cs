using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public bool hookable;
    public float pullForce;
    public float tickTime;
    private float timeCheck;
    private GameObject player;
    public LayerMask whatIsSolid;

    void Start()
    {
        hookable=false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && timeCheck < 0)
        {
            Debug.Log("Hooked");
            player = other.gameObject;
            hookable = true;
            gameObject.transform.up = new Vector2(Vector2.up.x-180f, Vector2.up.y-180f);
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
    void OnDestroy()
    {
        if (hookable)
        {
            player.transform.position = gameObject.transform.position;
        }
    }
}
