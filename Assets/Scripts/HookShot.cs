using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // ��������� �����������
    public float cooldownTime = 5f;
    public float pullRadius = 5f;
    public float pullForce = 10f;
    private float buffer = 0;
    // ����� �������� ���� ����������
    public float pullDuration = 1f;

    // ����� �� ��������� ���������
    private float nextActivationTime;
    // ����� ��������� �������� ���� ����������
    private float pullEndTime;

    public void ActivateAbility()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        if (Vector2.Distance(transform.position, player.transform.position) <= pullRadius)
        {
            buffer = playerMovement.moveSpeed;
            playerMovement.moveSpeed = 0;
            // ��������� ����������
            pullEndTime = Time.time + pullDuration;

            // ��������� ���� ���������� � ����������
            while (pullEndTime >= 0)
            {
                ApplyPullForce();
                pullEndTime -= Time.deltaTime;
            }
            pullEndTime = pullDuration;
            playerMovement.moveSpeed = buffer;
        }
    }
    public void DeactivateAbility()
    {
        playerMovement.moveSpeed = buffer;
    }
        // ����� ��� ���������� ���� ���������� � ����������
    private void ApplyPullForce()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) <= pullRadius && Time.time < pullEndTime)
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;
            player.GetComponent<Rigidbody2D>().AddForce(direction * pullForce);
        }
    }

    // ���������� ���������
    public void Update()
    {
        // �������� ������� ��� ��������� �����������
        if (nextActivationTime < 0)
        {
            ActivateAbility();
            nextActivationTime = cooldownTime;
        }
        else nextActivationTime -= Time.deltaTime;
    }
}
