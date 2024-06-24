using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthChanger>().TakeDamage(-10);
        }
    }
    void Start()
    {
        Debug.Log("It's tile behaviour 2!");        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
