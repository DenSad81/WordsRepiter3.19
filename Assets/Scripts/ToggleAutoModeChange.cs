using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAutoModeChange : MonoBehaviour
{
    private Toggle _toggleMode;
    private bool _isAutoMode = true;

    public bool IsAutoModeChange => _isAutoMode;

    private void Start()
    {
        _toggleMode = GetComponent<Toggle>();
    }

    private void Update()
    {
        _isAutoMode = _toggleMode.isOn;
    }





}
