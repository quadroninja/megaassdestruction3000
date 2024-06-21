using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker : Enemy
{
    // �������� ���������� �����������
    public HookShot hookShot;

    void Start()
    {
        // ������������� �����������
        hookShot = GetComponent<HookShot>();
    }

    void Update()
    {
        // ����� ������ Update() �������� ������ Enemy
        base.Update();
        // ���������� ��������� �����������
        hookShot.Update();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }
}
