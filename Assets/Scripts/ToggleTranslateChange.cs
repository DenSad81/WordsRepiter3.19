using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleTranslateChange : MonoBehaviour
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
        _mainProcess.IsTranslateRevers = toggleState;
    }
}
