using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlUp : MonoBehaviour
{
    public GameObject player;
    public GameObject lvlUpWindow;
    public Button[] options;
    public List<GameObject> weapons;
    public WeaponInventory inventory;
    private List<int> usedIndexes = new List<int>();
    private List<int> usedInventoryIndexes = new List<int>();

    public void Equip(GameObject weapon)
    {
        Debug.Log("Equip");
        inventory.addWeapon(weapon);
        foreach (Button button in options)
        {
            button.gameObject.SetActive(false);
        }
        weapons.Remove(weapon);
    }
    public void Upgrade(int index)
    {
        Debug.Log("Upgraded "+ index);
        inventory.upgradeWeapon(index);
        foreach (Button button in options)
        {
            button.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        SetActive();
    }
    public void SetActive()
    {
        lvlUpWindow.SetActive(true);
        foreach (Button button in options)
        {
            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
        }
        if (inventory.Weapons.Count == inventory.Capacity)
        {
            weapons.Clear();
        }
        usedIndexes.Clear();
        usedInventoryIndexes.Clear();
        int cnt = 0;
        foreach (GameObject weapon in inventory.Weapons)
        {
            if (weapon.GetComponent<WeaponBehaviour>().level == 5)
            {
                usedInventoryIndexes.Add(cnt);
            }
            cnt++;
        }
        Time.timeScale = 0f;
        foreach (Button button in options)
        {
            int randomIndex = GetUniqueRandomIndex(weapons, usedIndexes);
            if (randomIndex != -1)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = weapons[randomIndex].name;//randomIndex.ToString();
                //button.GetComponent<Image>().sprite = weapons[randomIndex].GetComponent<SpriteRenderer>().sprite;
                button.onClick.AddListener(() => Equip(weapons[randomIndex]));
            }
            else
            {
                int randomUpgradeIndex = GetUniqueRandomIndex(inventory.Weapons, usedInventoryIndexes);
                if (randomUpgradeIndex != -1)
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade " + inventory.Weapons[randomUpgradeIndex].name;
                    button.onClick.AddListener(() => Upgrade(randomUpgradeIndex));
                }
                else button.gameObject.SetActive(false);
            }
        }
    }

    public void OnClick()
    {
        lvlUpWindow.SetActive(false);
        Time.timeScale = 1f;
    }

    private int GetUniqueRandomIndex(List<GameObject> list, List<int> Indexes)
    {
        if (list.Count == 0)
        {
            return -1; // Нет доступных оружий
        }

        int cnt = 0;
        loop1: int randomIndex = Random.Range(0, list.Count);
        foreach(int index in Indexes)
        {
            if (cnt > 20)
            {
                return -1;
            }
            if (randomIndex == index)
            {
                cnt++;
                goto loop1;
            }
        }
        Indexes.Add(randomIndex);
        return randomIndex;
    }
}
