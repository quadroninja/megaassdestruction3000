using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker : Enemy
{
    // �������� ���������� �����������
    public HookShot hookShot;

    void Start()
    {
        base.Start();
        // ������������� �����������
        hookShot = GetComponent<HookShot>();
    }
    void FixedUpdate()
    {
        base.Update();
        base.FixedUpdate();
    }
}
