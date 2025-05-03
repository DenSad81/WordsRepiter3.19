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

public static class MainProcces /*: MonoBehaviour*/
{
    private static WorkWithDB _workWithDB;
    private static Utils _utils = new Utils();

    private static List<int> _poolIDsQuestions = new List<int>();
    private static List<int> _poolIDsAnswers = new List<int>();   
    public static int[] AnswersID = new int[9];

    public static int VolumeOfPoolIDs => _poolIDsQuestions.Count;
    public static int WordIdInDB => AnswersID[0];// _wordIdInDB;

    public static void FirstScan(object obj)
    {
        _workWithDB = (WorkWithDB)obj;

        FillingPools();
        ProccesBody();
    }

    private static void FillingPools()
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

    public static void ProccesBody()
    {
        if (_workWithDB.GetWordFromDB(AnswersID[0]).CorrectAnswers <= 0)
            _poolIDsQuestions.Remove(AnswersID[0]);

        EventsManager.EventDoResetColor?.Invoke();
        FillingFields();
        RandomisationFields();
        EventsManager.EventDoPrint?.Invoke();
    }

    private static void FillingFields()
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

    private static void RandomisationFields()
    {
        int pos = Random.Range(1, AnswersID.Length);
        AnswersID[pos] = AnswersID[0];
    }

    public static bool ChekIfAnswerIsRight(int fieldIndex) =>
         (AnswersID[fieldIndex] == WordIdInDB);//угадали;
}
