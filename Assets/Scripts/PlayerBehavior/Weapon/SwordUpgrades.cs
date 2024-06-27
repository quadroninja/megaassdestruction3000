using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUpgrades : WeaponBehaviour
{
    public WeaponInventory inventory;
    public float scale;
    // Start is called before the first frame update
    void Start()
    {
        inventory= GameObject.FindGameObjectWithTag("WeaponInventory").GetComponent<WeaponInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        base.bulletPrefab.transform.localScale = new Vector3((base.level*scale), (base.level*scale), 1f);
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
            base.speed = 3f;
            base.lifetime = 0.7f;
        }
    }
}
