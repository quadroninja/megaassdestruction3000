using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemy
{
    // �������� ���������� �����������
    public Explode explode;

    void Start()
    {
        // ������������� �����������
        explode = GetComponent<Explode>();
    }

    void Update()
    {
        // ����� ������ Update() �������� ������ Enemy
        base.Update();
        // ���������� ��������� �����������
        explode.Update();
        if (base.health < 0)
        {
            explode.ActivateExplodeAbility();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
}
