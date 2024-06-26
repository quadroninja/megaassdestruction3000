using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgrades : WeaponBehaviour
{
    public WeaponInventory a;
    // Start is called before the first frame update
    void Start()
    {
        a= GameObject.FindGameObjectWithTag("WeaponInventory").GetComponent<WeaponInventory>();
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
            a.addExtra(gameObject);
        }
    }
}
