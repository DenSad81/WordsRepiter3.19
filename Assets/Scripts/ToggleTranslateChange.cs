//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ToggleTranslateChange : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Toggle _toggle;

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
        SettingHolder.IsTranslateRevers = toggleState;
    }

    private void Start()
    {
        _toggle.isOn = SettingHolder.IsTranslateRevers;
    }
}
