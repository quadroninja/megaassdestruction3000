using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : Enemy
{
    // �������� ���������� �����������
    public Swap swap;

    void Start()
    {
        base.Start();
        // ������������� �����������
        swap = GetComponent<Swap>();
    }
    void FixedUpdate()
    {
        base.Update();
        base.FixedUpdate();
    }
}
