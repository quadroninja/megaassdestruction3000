using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public float damping = 1.5f;
    public GameObject p;
    private Transform player;
    private int lastX;

    void Start()
    {
        FindPlayer();
    }

    public void FindPlayer()
    {
        player = p.transform;
        lastX = Mathf.RoundToInt(player.position.x);
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    void Update()
    {
        if (player)
        {
            Vector3 target;
            target = new Vector3(player.position.x, player.position.y, transform.position.z);
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
