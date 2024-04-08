//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Field : MonoBehaviour
{
    [SerializeField] private MainProcces _mainProcces;
    [SerializeField] private int _fieldIndex;
    [SerializeField] private bool _isQuestionField;
    [SerializeField] private bool _isAnswerField;
    [SerializeField] private bool _isWordField;
    [SerializeField] private bool _isRightAnswerField;
    [SerializeField] private bool _isQuantityField;

    private TMP_Text _text;
    private Button _button;
    private Image _image;
    private Color _colorSemiRed;
    private Color _colorSemiGreen;
    private Color _colorTransend;

    private void Awake() {
        _text = GetComponent<TMP_Text>();
        _button = GetComponent<Button>();
        _image = GetComponentInChildren<Image>();
    }

    private void OnEnable() {
        _button.onClick.AddListener(OnButtonClick);
        _mainProcces.EventDoResetColor += DoResetColor;
        _mainProcces.EventDoPrint += DoPrint;
        _mainProcces.EventDoPrintAddictionalField += DoPrintAddictionalField;
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(OnButtonClick);
        _mainProcces.EventDoResetColor -= DoResetColor;
        _mainProcces.EventDoPrint -= DoPrint;
        _mainProcces.EventDoPrintAddictionalField -= DoPrintAddictionalField;
    }

    private void OnButtonClick() {
        int codOfColor = _mainProcces.GetColor(_fieldIndex, _isAnswerField);

        if (codOfColor == 1)
            _image.color = _colorSemiGreen;

        if (codOfColor == 2)
            _image.color = _colorSemiRed;

        _mainProcces.StartNewProcces(_fieldIndex, _isQuestionField, _isAnswerField);
    }

    public void DoResetColor() {
        _image.color = _colorTransend;
    }

    public void DoPrint() {
        if (_isQuestionField)
            _text.text = _mainProcces.Word;

        if (_isAnswerField)
            _text.text = _mainProcces._answersWord[_fieldIndex];

        if (_isQuantityField)
            _text.text = _mainProcces.VolumeOfPoolIDs.ToString();
    }

    public void DoPrintAddictionalField() {
        if (_isRightAnswerField)
            _text.text = _mainProcces.RightWord;

        if (_isWordField)
            _text.text = _mainProcces.Word;
    }

    void Start() {
        _colorSemiRed = Color.red;
        _colorSemiRed.a = 0.3f;
        _colorSemiGreen = Color.green;
        _colorSemiGreen.a = 0.3f;
        _colorTransend = new Color(0f, 0f, 0f, 0f);
    }
}
