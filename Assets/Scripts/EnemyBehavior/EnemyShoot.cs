using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    Rigidbody2D rb;
    Camera SceneCamera;
    public List<GameObject> spawned;
    public GameObject bullet;
    public Transform player;
    public float spawnRadius = 0f;
    public float offset;
    private float timeCheck;
    public float atkSpeed;
    public int maxspawn = 10;
    private int cnt=0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = SceneCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = player.position - transform.position;
        float EnemyRotation = Mathf.Atan2(diff.y,  diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, EnemyRotation-offset);
        if (Vector2.Distance(transform.position, player.transform.position) <= spawnRadius)
        {
            DestroyAllSpawned();
        }
        if (timeCheck <= 0)
        {
            if (Input.GetMouseButton(0) && Vector2.Distance(transform.position, player.transform.position) > spawnRadius && cnt<=maxspawn)
            {
                GameObject spw = Instantiate(bullet, transform.position, transform.rotation);
                spawned.Add(spw);
                timeCheck = atkSpeed;
                cnt++;
            }
        }
        else timeCheck -= Time.deltaTime;
    }

    public void DestroyAllSpawned()
    {
        spawned.ForEach(Destroy);
        spawned.Clear();
        cnt = 0;
    }
}
