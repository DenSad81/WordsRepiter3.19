//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;

public class ToggleModeChange : MonoBehaviour
{
    [SerializeField] private MainProcces _mainProcess;
    [SerializeField] private Toggle _toggle;

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
        _mainProcess.IsAutoMode = toggleState;
    }


}
