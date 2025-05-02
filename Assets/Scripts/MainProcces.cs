//using System;
//using System.Collections;
using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Threading;
//using TMPro;
//using Unity.Collections.LowLevel.Unsafe;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class MainProcces : MonoBehaviour
{
    [SerializeField] private WorkWithDB _workWithDB;

    private List<int> _poolIDsQuestions = new List<int>();
    private List<int> _poolIDsAnswers = new List<int>();
    private Utils _utils = new Utils();

    public int[] AnswersID = new int[9];

    public int VolumeOfPoolIDs => _poolIDsQuestions.Count;
    public int WordIdInDB => AnswersID[0];// _wordIdInDB;

    public event UnityAction EventDoResetColor;
    public event UnityAction EventDoPrint;
    public event UnityAction EventDoPrintAddictionalField;

    private void Start()
    {
        FillingPools();
        ProccesBody();
    }

    private void FillingPools()
    {
        _poolIDsQuestions.Clear();
        _poolIDsAnswers.Clear();

        int k = 1;

        while (true)
        {
            Word tempWord = _workWithDB.GetWordFromDB(k);

            if (tempWord == null)
                return;

            if (tempWord.CorrectAnswers != 0)
                _poolIDsQuestions.Add(tempWord.Id);

            _poolIDsAnswers.Add(tempWord.Id);
            k++;
        }
    }

    //private void FillingPools3()
    //{
    //    _poolIDsQuestions.Clear();
    //    _poolIDsAnswers.Clear();

    //    int k = 1;

    //    while (_workWithDB.GetWordFromDB(k) != null)
    //    {
    //        if (_workWithDB.GetWordFromDB(k).CorrectAnswers != 0)
    //            _poolIDsQuestions.Add(_workWithDB.GetWordFromDB(k).Id);

    //        _poolIDsAnswers.Add(_workWithDB.GetWordFromDB(k).Id);
    //        k++;
    //    }
    //}

    //private void FillingQuestionsPool()
    //{
    //    _poolIDsQuestions.Clear();

    //    int k = 1;

    //    while (_workWithDB.GetWordFromDB(k) != null)
    //    {
    //        if (_workWithDB.GetWordFromDB(k).CorrectAnswers != 0)
    //            _poolIDsQuestions.Add(_workWithDB.GetWordFromDB(k).Id);

    //        k++;
    //    }
    //}

    //private void FillingAnswersPool()
    //{
    //    _poolIDsAnswers.Clear();

    //    int k = 1;

    //    while (_workWithDB.GetWordFromDB(k) != null)
    //    {
    //        _poolIDsAnswers.Add(_workWithDB.GetWordFromDB(k).Id);
    //        k++;
    //    }
    //}

    //public void ReStart()
    //{
    //    _workWithDB.SetAllCorrectAnswersInTableWords(SettingHolder.QuantityRepit);

    //    FillingQuestionsPool();
    //    FillingAnswersPool();
    //    ProccesBody();
    //}

    private void ProccesBody()
    {
        if (_workWithDB.GetWordFromDB(AnswersID[0]).CorrectAnswers <= 0)
            _poolIDsQuestions.Remove(AnswersID[0]);

        EventDoResetColor?.Invoke();
        FillingFields();
        RandomisationFields();
        Print();
    }

    private void FillingFields()
    {
        if (_poolIDsQuestions.Count < 1)
            return;

        AnswersID[0] = _poolIDsQuestions[Random.Range(0, _poolIDsQuestions.Count)];
        _utils.GetUnicElementFromCollection(_poolIDsAnswers, false, AnswersID[0]);

        AnswersID[1] = _utils.GetUnicElementFromCollection(_poolIDsAnswers);
        AnswersID[2] = _utils.GetUnicElementFromCollection(_poolIDsAnswers);
        AnswersID[3] = _utils.GetUnicElementFromCollection(_poolIDsAnswers);
        AnswersID[4] = _utils.GetUnicElementFromCollection(_poolIDsAnswers);
        AnswersID[5] = _utils.GetUnicElementFromCollection(_poolIDsAnswers);
        AnswersID[6] = _utils.GetUnicElementFromCollection(_poolIDsAnswers);
        AnswersID[7] = _utils.GetUnicElementFromCollection(_poolIDsAnswers);
        AnswersID[8] = _utils.GetUnicElementFromCollection(_poolIDsAnswers);
    }

    private void RandomisationFields()
    {
        int pos = Random.Range(1, AnswersID.Length);
        AnswersID[pos] = AnswersID[0];
    }

    private void Print() =>
        EventDoPrint?.Invoke();

    public void PrintAddictionalField() =>
        EventDoPrintAddictionalField?.Invoke();

    public void AddToPoolRightAnswers() =>
       _workWithDB.IncreaseCorrectAnswersInTableWords(AnswersID[0]);

    public void SubtractToPoolRightAnswers() =>
        _workWithDB.DecreaseCorrectAnswersInTableWords(AnswersID[0]);

    public int GetColor(int fieldIndex, bool isAnswerField)
    {
        int codOfColor = -1;

        if (isAnswerField)
        {
            if (AnswersID[fieldIndex] == WordIdInDB)//угадали
                codOfColor = 1; // semi green // _image.color = _colorSemiGreen;           
            else
                codOfColor = 2; // semi red // _image.color = _colorSemiRed;
        }

        return codOfColor;
    }

    public void StartNewProcces(int fieldIndex, bool isQuestionField, bool isAnswerField)
    {
        if (isQuestionField)
        {
            if (_workWithDB.ChekIfModeAutoFromTableUsers() == false)
                ProccesBody();
        }

        if (isAnswerField)
        {
            if (AnswersID[fieldIndex] == WordIdInDB)//угадали
            {
                PrintAddictionalField();
                SubtractToPoolRightAnswers();

                if (_workWithDB.ChekIfModeAutoFromTableUsers())
                    ProccesBody();
            }
            else
            {
                AddToPoolRightAnswers();
            }
        }
    }
}
