using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemy
{
    // �������� ���������� �����������
    public Explode explode;

    void Start()
    {
        base.Start();
        // ������������� �����������
        explode = GetComponent<Explode>();
    }

    void FixedUpdate()
    {
        // ����� ������ Update() �������� ������ Enemy
        base.Update();
        base.FixedUpdate();
        // ���������� ��������� �����������
        explode.Update();
        if (base.health < 0)
        {
            explode.ActivateExplodeAbility();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
}
