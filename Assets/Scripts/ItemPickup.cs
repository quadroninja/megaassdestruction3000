using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private InventoryCheck inventory;
    public GameObject slot;
    public GameObject player;

    private void Start()
    {
        inventory = player.GetComponent<InventoryCheck>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            for (int j = 0; j < inventory.full.Length; j++)
            {
                if (inventory.full[j] == false)
                {
                    Destroy(gameObject);
                    Instantiate(slot, inventory.item[j].transform);
                    inventory.full[j] = true;
                    break;
                }
            }
        }
    }
}
