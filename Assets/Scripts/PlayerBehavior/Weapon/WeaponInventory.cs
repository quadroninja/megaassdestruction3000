using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponInventory : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<GameObject> extra;
    public List<GameObject> Weapons { get { return weapons; } }
    public List<GameObject> Extra { get { return extra; } }
    private int capacity = 3;
    //public int Capacity { get { return capacity; } }

    public void addExtra(GameObject weapon)
    {
        GameObject newWeapon = Instantiate(weapon, transform.position, Quaternion.identity, transform);
        extra.Add(newWeapon);
        Debug.Log("Added weapon: " + newWeapon.name);
    }

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
    public void upgradeWeapon(int index)
    {
        weapons[index].GetComponent<WeaponBehaviour>().newlevel();
        Debug.Log(weapons[index].GetComponent<WeaponBehaviour>().level);
    }
}
