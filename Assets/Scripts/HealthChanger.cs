using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChanger : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private float timeCheck;
    public float invisibilityTime;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.ChangeMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        if (timeCheck <= 0)
        {
            currentHealth -= damage;
            healthBar.ChangeHealth(currentHealth);
            timeCheck = invisibilityTime;
        }
        else timeCheck -= Time.deltaTime;
    }
}
