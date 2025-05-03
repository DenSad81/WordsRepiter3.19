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
    //[SerializeField] private MainProcces _mainProcces;
    [SerializeField] private int _fieldIndex;
    [SerializeField] private bool _isQuestionField;
    [SerializeField] private bool _isAnswerField;
    [SerializeField] private bool _isWordField;
    [SerializeField] private bool _isRightAnswerField;
    [SerializeField] private bool _isQuantityField;
    [SerializeField] private WorkWithDB _workWithDB;
    //[SerializeField] private EventsManager _eventsManager;

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

        if (_isAnswerField)
            EventsManager.EventDoResetColor += DoResetColor;

        if (_isQuestionField || _isAnswerField || _isQuantityField)
            EventsManager.EventDoPrint += DoPrintMainFields;

        if (_isRightAnswerField || _isWordField)
            EventsManager.EventDoPrintAddictionalField += DoPrintAddictionalFields;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);

        if (_isAnswerField)
            EventsManager.EventDoResetColor -= DoResetColor;

        if (_isQuestionField || _isAnswerField || _isQuantityField)
            EventsManager.EventDoPrint -= DoPrintMainFields;

        if (_isRightAnswerField || _isWordField)
            EventsManager.EventDoPrintAddictionalField -= DoPrintAddictionalFields;
    }

    void Start()
    {
        _colorSemiRed = Color.red;
        _colorSemiRed.a = 0.3f;
        _colorSemiGreen = Color.green;
        _colorSemiGreen.a = 0.3f;
        _colorTransend = new Color(0f, 0f, 0f, 0f);
    }

    private void OnButtonClick()
    {
        Debug.Log("rgtg");

        if (_isAnswerField)//мен€ем цвет пол€ 
        {
            if (MainProcces.ChekIfAnswerIsRight(_fieldIndex))//угадали
                _image.color = _colorSemiGreen;
            else
                _image.color = _colorSemiRed;
        }



        if (_isQuestionField)//запуск нового цикла
        {
            if (_workWithDB.ChekIfModeAutoFromTableUsers() == false)//нужно упростить чтоб посто€нно в DB не лезть
                MainProcces.ProccesBody();
        }
        else if (_isAnswerField)
        {
            if (MainProcces.ChekIfAnswerIsRight(_fieldIndex))//угадали
            {
                EventsManager.EventDoPrintAddictionalField?.Invoke();

                //DoPrintAddictionalFields();

                _workWithDB.DecreaseCorrectAnswersInTableWords(MainProcces.AnswersID[0]);

                if (_workWithDB.ChekIfModeAutoFromTableUsers())
                    MainProcces.ProccesBody();
            }
            else
            {
                _workWithDB.IncreaseCorrectAnswersInTableWords(MainProcces.AnswersID[0]);
            }
        }
    }

    public void DoResetColor() =>
        _image.color = _colorTransend;

    public void DoPrintMainFields()
    {
        if (_isQuestionField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromDB(MainProcces.WordIdInDB).WordEn;
            else
                _text.text = _workWithDB.GetWordFromDB(MainProcces.WordIdInDB).WordRu;

        if (_isAnswerField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromDB(MainProcces.AnswersID[_fieldIndex]).WordRu;
            else
                _text.text = _workWithDB.GetWordFromDB(MainProcces.AnswersID[_fieldIndex]).WordEn;

        if (_isQuantityField)
            _text.text = MainProcces.VolumeOfPoolIDs.ToString();
    }

    public void DoPrintAddictionalFields()
    {
        if (_isRightAnswerField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromDB(MainProcces.WordIdInDB).WordRu;
            else
                _text.text = _workWithDB.GetWordFromDB(MainProcces.WordIdInDB).WordEn;

        if (_isWordField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromDB(MainProcces.WordIdInDB).WordEn;
            else
                _text.text = _workWithDB.GetWordFromDB(MainProcces.WordIdInDB).WordRu;
    }
}
