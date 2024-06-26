using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgrades : WeaponBehaviour
{
    public WeaponInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory= GameObject.FindGameObjectWithTag("WeaponInventory").GetComponent<WeaponInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (base.upgradable)
        {
            base.upgradable = false;
            coreFeatures();
        }
    }

    public void coreFeatures()
    {
        if (base.level == 4)
        {
            inventory.addExtra(gameObject);
        }
    }
}
