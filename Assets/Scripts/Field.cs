//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;

public class Field : MonoBehaviour
{
    [SerializeField] private MainProcces _mainProcces;
    [SerializeField] private int _fieldIndex;
    [SerializeField] private bool _isQuestionField;
    [SerializeField] private bool _isAnswerField;
    [SerializeField] private bool _isWordField;
    [SerializeField] private bool _isRightAnswerField;
    [SerializeField] private bool _isQuantityField;
    [SerializeField] private WorkWithDB _workWithDB;

    private TMP_Text _text;
    private Button _button;
    private Image _image;
    private Color _colorSemiRed;
    private Color _colorSemiGreen;
    private Color _colorTransend;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _button = GetComponent<Button>();
        _image = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _mainProcces.EventDoResetColor += DoResetColor;
        _mainProcces.EventDoPrint += DoPrint;
        _mainProcces.EventDoPrintAddictionalField += DoPrintAddictionalField;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _mainProcces.EventDoResetColor -= DoResetColor;
        _mainProcces.EventDoPrint -= DoPrint;
        _mainProcces.EventDoPrintAddictionalField -= DoPrintAddictionalField;
    }

    private void OnButtonClick()
    {
        int codOfColor = _mainProcces.GetColor(_fieldIndex, _isAnswerField);

        if (codOfColor == 1)
            _image.color = _colorSemiGreen;

        if (codOfColor == 2)
            _image.color = _colorSemiRed;

        _mainProcces.StartNewProcces(_fieldIndex, _isQuestionField, _isAnswerField);
    }

    public void DoResetColor()
    {
        _image.color = _colorTransend;
    }

    public void DoPrint()
    {
        if (_isQuestionField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromDB(_mainProcces.WordIdInDB).WordEn;
            else
                _text.text = _workWithDB.GetWordFromDB(_mainProcces.WordIdInDB).WordRu;

        if (_isAnswerField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromDB(_mainProcces.AnswersID[_fieldIndex]).WordRu;
            else
                _text.text = _workWithDB.GetWordFromDB(_mainProcces.AnswersID[_fieldIndex]).WordEn;

        if (_isQuantityField)
            _text.text = _mainProcces.VolumeOfPoolIDs.ToString();
    }

    public void DoPrintAddictionalField()
    {
        if (_isRightAnswerField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromDB(_mainProcces.WordIdInDB).WordRu;
            else
                _text.text = _workWithDB.GetWordFromDB(_mainProcces.WordIdInDB).WordEn;

        if (_isWordField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromDB(_mainProcces.WordIdInDB).WordEn;
            else
                _text.text = _workWithDB.GetWordFromDB(_mainProcces.WordIdInDB).WordRu;
    }

    void Start()
    {
        _colorSemiRed = Color.red;
        _colorSemiRed.a = 0.3f;
        _colorSemiGreen = Color.green;
        _colorSemiGreen.a = 0.3f;
        _colorTransend = new Color(0f, 0f, 0f, 0f);
    }
}
