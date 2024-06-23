using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public GameObject[] weapons;
    public AutoWeapon weaponStorage;
    private GameObject buffer;
    void Start()
    {
        int randomIndex = Random.Range(0, weapons.Length);
        GetComponent<Image>().sprite = weapons[randomIndex].GetComponent<SpriteRenderer>().sprite;
        buffer = weapons[randomIndex];
    }
    public void OnClick()
    {
        weaponStorage.addWeapon(buffer);
    }
}
