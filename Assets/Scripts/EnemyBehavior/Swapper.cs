using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : Enemy
{
    // �������� ���������� �����������
    public Swap swap;

    void Start()
    {
        // ������������� �����������
        swap = GetComponent<Swap>();
    }
}
