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
        transform.LookAt(player);
        if (Vector2.Distance(transform.position, player.transform.position) <= spawnRadius)
        {
            DestroyAllSpawned();
        }
        if (timeCheck <= 0)
        {
            GameObject spw = Instantiate(bullet, transform.position, transform.rotation);
            spawned.Add(spw);
            Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            spw.transform.up = direction;
            timeCheck = atkSpeed;
            cnt++;
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
