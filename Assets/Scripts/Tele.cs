using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthChanger>().TakeDamage(50);
            other.transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
