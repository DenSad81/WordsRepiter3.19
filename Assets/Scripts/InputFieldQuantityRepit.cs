//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldQuantityRepit : MonoBehaviour
{
    private TMP_InputField _inputField;

    private WorkWithDB _workWithDB;

    private void Awake()=>
        _inputField = GetComponent<TMP_InputField>();

    private void OnEnable()=>
        _inputField.onEndEdit.AddListener(OnEndInput);

    private void OnDisable()=>
        _inputField.onValueChanged.RemoveListener(OnEndInput);

    public void OnEndInput(string str)
    {
        if (int.Parse(str) < 1 || int.Parse(str) > 11)
            return;

        _workWithDB.SetQuantityRepitInTableUsers(int.Parse(str));
    }

    private void Start()
    {
        _workWithDB = GameObject.Find("WorkWithDB").GetComponent<WorkWithDB>();

        _inputField.text = _workWithDB.GetQuantityRepitFromTableUsers().ToString();
    }
}
