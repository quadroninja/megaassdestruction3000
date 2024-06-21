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
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        timeCheck -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (timeCheck <= 0)
        {
            currentHealth -= damage;
            healthBar.ChangeHealth(currentHealth);
            timeCheck = invisibilityTime;
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
