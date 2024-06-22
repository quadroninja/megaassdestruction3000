using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlUp : MonoBehaviour
{
    public GameObject lvlUpWindow;
    public Button startButton;

    void Start()
    {
        lvlUpWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClick()
    {
        lvlUpWindow.SetActive(false);
        Time.timeScale = 1f;
    }
}
