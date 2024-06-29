using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthChanger : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private float timeCheck;
    public float invisibilityTime;
    public float regenCheck;
    public float healingTime;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.ChangeMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Regen();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        timeCheck -= Time.deltaTime;
    }
    public void Regen()
    {
        if (currentHealth + 10 < maxHealth) { regenCheck -= Time.deltaTime; }
        if (regenCheck <= 0 && currentHealth+10 < maxHealth)
        {
            currentHealth += 10;
            healthBar.ChangeHealth(currentHealth);
            regenCheck = healingTime;
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
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Menu");
            Destroy(gameObject);
        }
    }
}
