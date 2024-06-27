using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapUpgrades : WeaponBehaviour
{
    public WeaponInventory inventory;
    public bool explodable;
    // Start is called before the first frame update
    void Start()
    {
        if (base.level == 1)
        {
            base.bulletPrefab.GetComponent<Trap>().change(false);
        }
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
            base.speed = 1f;
            base.speedScale = 1f;
            explodable = true;
        }
        base.bulletPrefab.GetComponent<Trap>().change(explodable);
    }
}
