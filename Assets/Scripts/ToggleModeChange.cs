//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Events;

public class ToggleModeChange : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Toggle _toggle;
    [SerializeField] private WorkWithDB _workWithDB;

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnToggleClick);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleClick);
    }

    private void OnToggleClick(bool toggleState)
    {
       // SettingHolder.IsAutoMode = toggleState;
        _workWithDB.SetModeAutoInTableUsers(toggleState);
    }

    private void Start()
    {
        //_toggle.isOn = SettingHolder.IsAutoMode;
        _toggle.isOn = _workWithDB.ChekIfModeAutoFromTableUsers();
    }
}
