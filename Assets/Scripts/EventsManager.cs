//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
using UnityEngine.Events;

public static class EventsManager /*: MonoBehaviour*/
{
    //public UnityAction EventDoSomthing1;//��������� ����� ������ � ����� ������
    //public event UnityAction EventDoSomthing2;//��������� ����� ������ � ������ ��� ������
    //public UnityEvent EventDoSomthing3;//�������� ����� AddListener � �� ����� ������������ (��� �� �����)

    public static /*event*/ UnityAction EventDoResetColor;
    public static /*event*/ UnityAction EventDoPrint;
    public static /*event*/ UnityAction EventDoPrintAddictionalField;
}
