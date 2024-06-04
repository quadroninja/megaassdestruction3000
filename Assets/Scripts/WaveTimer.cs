using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveTimer : MonoBehaviour
{
    public TMP_Text text;
    public EnemySpawner spawn;
    string time;
    // Update is called once per frame
    void Update()
    {
        time = string.Format("{0:N3}", spawn.timeRemaining);
        text.text = time;
    }
}
