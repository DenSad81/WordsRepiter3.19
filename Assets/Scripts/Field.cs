//using System;
using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
using TMPro;
using UnityEditor;

//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Events;

public class Field : MonoBehaviour
{
    [SerializeField] private int _fieldIndex;
    [SerializeField] private bool _isQuestionField;
    [SerializeField] private bool _isAnswerField;
    [SerializeField] private bool _isWordField;
    [SerializeField] private bool _isRightAnswerField;
    [SerializeField] private bool _isQuantityField;

    private WorkWithDB _workWithDB;
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

        _workWithDB = GameObject.Find("WorkWithDB").GetComponent<WorkWithDB>();
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
        if (_isQuestionField)
        {
            if (_workWithDB.ChekIfModeAutoFromTableUsers() == false)//нужно упростить чтоб посто€нно в DB не лезть
            {
                MainProcces.CleanQuestionsPool();
                MainProcces.ProccesBody();
            }
        }
        else if (_isAnswerField)
        {
            if (MainProcces.ChekIfAnswerIsRight(_fieldIndex))//угадали
            {
                _image.color = _colorSemiGreen;
                EventsManager.EventDoPrintAddictionalField?.Invoke();
                _workWithDB.DecreaseCorrectAnswersInTableWords(MainProcces.AnswersID[0]);             

                if (_workWithDB.ChekIfModeAutoFromTableUsers() == true)
                {
                    //MainProcces.CleanQuestionsPool();
                    // MainProcces.ProccesBody();
                    //System.Threading.Thread.Sleep(5000); //millisec
                    StartCoroutine(StartNewCycle());
                }
            }
            else
            {
                _image.color = _colorSemiRed;
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
                _text.text = _workWithDB.GetWordFromTableWords(MainProcces.AnswersID[0]).WordEn;
            else
                _text.text = _workWithDB.GetWordFromTableWords(MainProcces.AnswersID[0]).WordRu;

        if (_isAnswerField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromTableWords(MainProcces.AnswersID[_fieldIndex]).WordRu;
            else
                _text.text = _workWithDB.GetWordFromTableWords(MainProcces.AnswersID[_fieldIndex]).WordEn;

        if (_isQuantityField)
            _text.text = MainProcces.VolumeOfPoolIDs.ToString();
    }

    public void DoPrintAddictionalFields()
    {
        if (_isRightAnswerField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromTableWords(MainProcces.AnswersID[0]).WordRu;
            else
                _text.text = _workWithDB.GetWordFromTableWords(MainProcces.AnswersID[0]).WordEn;

        if (_isWordField)
            if (_workWithDB.ChekIfDirectionEnRuFromTableUsers() == false)
                _text.text = _workWithDB.GetWordFromTableWords(MainProcces.AnswersID[0]).WordEn;
            else
                _text.text = _workWithDB.GetWordFromTableWords(MainProcces.AnswersID[0]).WordRu;
    }

    private  IEnumerator StartNewCycle()
    {
        yield return new WaitForSeconds(0.3f);

        MainProcces.CleanQuestionsPool();
        MainProcces.ProccesBody();
    }
}
