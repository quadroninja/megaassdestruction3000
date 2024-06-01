using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    Rigidbody2D rb;
    Camera SceneCamera;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = SceneCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimPos = mousePos - rb.position;
        float playerRotation = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        rb.rotation = playerRotation;
    }
}
