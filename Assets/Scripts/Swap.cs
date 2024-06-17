using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // ��������� �����������
    public float cooldownTime = 5f;
    public float swapRadius = 5f;

    private Vector3 swapBuffer;
    // ����� �� ��������� ���������
    private float nextActivationTime;

    public void ActivateSwapAbility()
    {
        ApplySwapForce();
    }
    private void ApplySwapForce()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) <= swapRadius)
        {
            swapBuffer = player.transform.position;
            player.transform.position = transform.position;
            transform.position = swapBuffer;
        }
    }

    // ���������� ���������
    public void Update()
    {
        // �������� ������� ��� ��������� �����������
        if (nextActivationTime < 0)
        {
            ActivateSwapAbility();
            nextActivationTime = cooldownTime;
        }
        else nextActivationTime -= Time.deltaTime;
    }
}
