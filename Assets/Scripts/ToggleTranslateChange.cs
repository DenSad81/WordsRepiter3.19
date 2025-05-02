//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ToggleTranslateChange : MonoBehaviour
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
        // SettingHolder.IsTranslateRevers = toggleState;
        _workWithDB.SetDirectionEnRuInTableUsers(toggleState);
    }

    private void Start()
    {
        // _toggle.isOn = SettingHolder.IsTranslateRevers;
        _toggle.isOn = _workWithDB.ChekIfDirectionEnRuFromTableUsers();
    }
}
