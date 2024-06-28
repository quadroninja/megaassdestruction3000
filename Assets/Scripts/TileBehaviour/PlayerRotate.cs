using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    Rigidbody2D rb;
    Camera SceneCamera;
    public GameObject bullet;
    public Transform startPoint;
    public float offset;
    private float timeCheck;
    public float atkSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = SceneCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mousePos - transform.position;
        float playerRotation = Mathf.Atan2(diff.y,  diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, playerRotation- offset);

        if (timeCheck <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, startPoint.position, transform.rotation);
                timeCheck = atkSpeed;
            }
        }
        else timeCheck -= Time.deltaTime;
    }
}
