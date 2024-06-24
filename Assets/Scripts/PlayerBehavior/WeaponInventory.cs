using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponInventory : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<GameObject> Weapons { get { return weapons; } }
    private int capacity = 3;
    //public int Capacity { get { return capacity; } }


    public void addWeapon(GameObject weapon)
    {
        Debug.Log(weapons.Count);
        if (weapons.Count < capacity)
        {
            GameObject newWeapon = Instantiate(weapon, transform.position, Quaternion.identity, transform);
            weapons.Add(newWeapon);
            Debug.Log("Added weapon: " + newWeapon.name);
        }
    }
}
