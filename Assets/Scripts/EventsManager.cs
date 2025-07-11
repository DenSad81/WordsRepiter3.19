//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
using UnityEngine.Events;

public static class EventsManager /*: MonoBehaviour*/
{
    //public UnityAction EventDoSomthing1;//выполнить можно только в любом классе
    //public event UnityAction EventDoSomthing2;//выполнить можно только в классе где создан
    //public UnityEvent EventDoSomthing3;//подписка через AddListener и не нужно отписываться (это не точно)

    public static /*event*/ UnityAction EventDoResetColor;
    public static /*event*/ UnityAction EventDoPrint;
    public static /*event*/ UnityAction EventDoPrintAddictionalField;
}
