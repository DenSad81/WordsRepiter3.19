//using System.Collections;
//using System.Collections.Generic;
//using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Dictionary
{
    public Dictionary(int id, int dictId, string dictName, string state)
    {
        Id = id;
        DictId = dictId;
        DictName = dictName;
        State = state;
    }

    public int Id { get; set; }
    public int DictId { get; set; }
    public string DictName { get; set; }
    public string State { get; set; }


    public void ShowData() =>
    Debug.Log($"{Id} {DictId} {DictName} {State}");

}
