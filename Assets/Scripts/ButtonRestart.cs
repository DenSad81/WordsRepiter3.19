//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRestart : MonoBehaviour
{
    // [SerializeField] private MainProcces _mainProcces;
    [SerializeField] private WorkWithDB _workWithDB;
    private Button _button;

    private void Awake() =>
        _button = GetComponent<Button>();

    private void OnEnable() =>
        _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick() =>
        _workWithDB.SetAllCorrectAnswersInTableWords(SettingHolder.QuantityRepit);
}
