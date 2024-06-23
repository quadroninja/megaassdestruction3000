using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlUp : MonoBehaviour
{
    public GameObject lvlUpWindow;
    public Button[] options;
    public List<GameObject> weapons;
    public AutoWeapon weaponStorage;
    private List<int> usedIndexes = new List<int>();

    public void Equip(GameObject weapon)
    {
        weaponStorage.addWeapon(weapon);
        foreach (Button button in options)
        {
            button.gameObject.SetActive(false);
        }
        weapons.Remove(weapon);
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
        usedIndexes.Clear();
        Time.timeScale = 0f;
        foreach (Button button in options)
        {
            int randomIndex = GetUniqueRandomIndex();
            if (randomIndex != -1)
            {
                button.GetComponent<Image>().sprite = weapons[randomIndex].GetComponent<SpriteRenderer>().sprite;
                button.onClick.AddListener(() => Equip(weapons[randomIndex]));
            }
            else button.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        lvlUpWindow.SetActive(false);
        Time.timeScale = 1f;
    }

    private int GetUniqueRandomIndex()
    {
        if (weapons.Count == 0)
        {
            return -1; // Нет доступных оружий
        }

        int cnt = 0;
        loop1: int randomIndex = Random.Range(0, weapons.Count);
        foreach(int index in usedIndexes)
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
        usedIndexes.Add(randomIndex);
        return randomIndex;
    }
}
