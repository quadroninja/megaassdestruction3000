using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    public int experience = 0; // Текущий опыт игрока
    public int currentLevel = 1;
    public int[] experienceToLevelUp = { 5, 15, 300, 400 };
    public GameObject lvlUpWindow;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("xp"))
        {

            // Получить значение опыта из объекта
            int experienceObject = other.gameObject.GetComponent<Experience>().experienceValue;

            // Добавить опыт к игроку
            experience += experienceObject;

            Destroy(other);
        }
    }

    void Update()
    {
        UpdateLevel();
    }

    void UpdateLevel()
    {
        if (experience >= experienceToLevelUp[currentLevel - 1])
        {
            currentLevel++;
            experience -= experienceToLevelUp[currentLevel - 2];
            LevelUp();
        }
    }

    void LevelUp()
    {
        Debug.Log("Поздравляем! Вы достигли уровня " + currentLevel);
        lvlUpWindow.GetComponent<LvlUp>().SetActive();
    }
}
