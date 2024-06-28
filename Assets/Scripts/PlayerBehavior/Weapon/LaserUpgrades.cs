using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUpgrades : WeaponBehaviour
{
    public WeaponInventory inventory;
    public float baseRange;
    public float baseCooldown;
    public float laserScale;
    // Start is called before the first frame update
    void Start()
    {
        inventory= GameObject.FindGameObjectWithTag("WeaponInventory").GetComponent<WeaponInventory>();
    }
    void laserScalings()
    {
        base.bulletPrefab.GetComponent<Laser>().laserRange = baseRange + (baseRange * laserScale * (base.level - 1));
        base.bulletPrefab.GetComponent<Laser>().laserCooldown = baseCooldown + (baseCooldown * laserScale * (base.level - 1));
    }
    // Update is called once per frame
    void Update()
    {
        if (base.level == 1)
        {
            base.bulletPrefab.GetComponent<Laser>().change(false);
        }
        base.Update();
        if (base.upgradable)
        {
            base.upgradable = false;
            laserScalings();
            coreFeatures();
        }
    }

    public void coreFeatures()
    {
        if (base.level == 5)
        {
            base.baseSpeed = 3f;
            base.speed = 0f;
            base.bulletPrefab.GetComponent<Laser>().change(true);
        }
    }
}
