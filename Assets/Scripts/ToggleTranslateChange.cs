//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ToggleTranslateChange : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Toggle _toggle;

    private WorkWithDB _workWithDB;

    private void OnEnable()=>
        _toggle.onValueChanged.AddListener(OnToggleClick);

    private void OnDisable()=>
        _toggle.onValueChanged.RemoveListener(OnToggleClick);

    private void OnToggleClick(bool toggleState)=>
        _workWithDB.SetDirectionEnRuInTableUsers(toggleState);

    private void Start()
    {
        _workWithDB = GameObject.Find("WorkWithDB").GetComponent<WorkWithDB>();

        _toggle.isOn = _workWithDB.ChekIfDirectionEnRuFromTableUsers();
    }
}
