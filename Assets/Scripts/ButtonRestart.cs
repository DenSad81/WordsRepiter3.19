//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRestart : MonoBehaviour
{
    private Button _button;

    private WorkWithDB _workWithDB;

    private void Awake() =>
        _button = GetComponent<Button>();

    private void OnEnable() =>
        _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick() =>
        _workWithDB.SetAllCorrectAnswersInTableWords(_workWithDB.GetQuantityRepitFromTableUsers());

    private void Start() =>
        _workWithDB = GameObject.Find("WorkWithDB").GetComponent<WorkWithDB>();

}
