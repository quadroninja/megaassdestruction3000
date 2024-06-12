using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
    public GameObject[] weapons;
    public BulletBehavior stats;
    public PlayerRotate shoot;
    int wIndex = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot.bullet = weapons[wIndex];
            if (wIndex + 1 < weapons.Length)
            {
                wIndex++;
            }
            else wIndex = 0;
        }
    }
}
