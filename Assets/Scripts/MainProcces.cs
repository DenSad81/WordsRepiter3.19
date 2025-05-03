//using System;
//using System.Collections;
using System.Collections.Generic;
using TMPro;

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

    private void ProccesBody()
    {
        if (_workWithDB.GetWordFromDB(AnswersID[0]).CorrectAnswers <= 0)
            _poolIDsQuestions.Remove(AnswersID[0]);

        EventsManager.EventDoResetColor?.Invoke();
        FillingFields();
        RandomisationFields();
        EventsManager.EventDoPrint?.Invoke();
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
            if (_workWithDB.ChekIfModeAutoFromTableUsers() == false)//нужно упростить чтоб постоянно в DB не лезть
                ProccesBody();
        }

        if (isAnswerField)
        {
            if (AnswersID[fieldIndex] == WordIdInDB)//угадали
            {
                EventsManager.EventDoPrintAddictionalField?.Invoke();
                _workWithDB.DecreaseCorrectAnswersInTableWords(AnswersID[0]);

                if (_workWithDB.ChekIfModeAutoFromTableUsers())
                    ProccesBody();
            }
            else
            {
                _workWithDB.IncreaseCorrectAnswersInTableWords(AnswersID[0]);
            }
        }
    }
}
