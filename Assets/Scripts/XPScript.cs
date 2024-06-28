using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPScript : MonoBehaviour
{
    public int xpValue;
    private bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        isCollected = false;
    }

   /* void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.AddXP(xpValue);
            }

            gameObject.SetActive(false);

            isCollected = true;
        }
    }
   */
    // Update is called once per frame
    void Update()
    {
        
    }
}
