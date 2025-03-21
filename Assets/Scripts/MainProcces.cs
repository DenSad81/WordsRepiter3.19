//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainProcces : MonoBehaviour
{
    [SerializeField] private WorkWithDB _workWithDB;

    private List<int> _poolIDs = new List<int>();
    private List<int> _poolIDsConst = new List<int>();//copy _poolIDs

    private List<int> _poolIDsCopy = new List<int>();
    private int _posInPoolIDsCopy;

    private int _posInPoolIDs;

    public bool _isAutoMode = true;
    public int _quantityRepit = 1;
    public int[] _answersID = new int[9];

    public int VolumeOfPoolIDs => _poolIDs.Count;
    public int WordIdInDB => _answersID[0];// _wordIdInDB;

    public event UnityAction EventDoResetColor;
    public event UnityAction EventDoPrint;
    public event UnityAction EventDoPrintAddictionalField;

    private void Start()
    {
        ReStart();
    }

    public void ReStart()
    {
        _poolIDs.Clear();
        _poolIDsConst.Clear();

        for (int i = 1; i < 11; i++)
        {
            Word word = _workWithDB.GetWordFromDB(i);

            _poolIDs.Add(word.Id);
            _poolIDsConst.Add(word.Id);
            _workWithDB.SetCorrectAnswersInTableWords(word.Id, _quantityRepit);// количество верных ответов
        }

        EventDoResetColor?.Invoke();
        FillingWord();
        FillingAnswers();
        RandomisationAnswers();
        Print();
    }

    public void ProccesBody()
    {
        if (_workWithDB.GetWordFromDB(_answersID[0]/*_wordIdInDB*/).CorrectAnswers <= 0)
            _poolIDs.RemoveAt(_posInPoolIDs);

        EventDoResetColor?.Invoke();
        FillingWord();
        FillingAnswers();
        RandomisationAnswers();
        Print();
    }

    private void FillingWord()
    {
        _posInPoolIDs = Random.Range(0, _poolIDs.Count);

        _answersID[0] = _poolIDs[_posInPoolIDs];
        DebugCollection(0, _posInPoolIDs, _answersID[0], _poolIDs);
    }

    private void DebugCollection(int no, int pos, int id, List<int> collection)
    {
        string str = no + "   pos " + pos + "   id " + id + "  //  ";

        foreach (var item in collection)
            str = str + " " + item;

        Debug.Log(str);
    }

    private int GetUnicElementFromCollection(List<int> collection, List<int> copyCollection, bool reset)
    {
        if (reset == true || copyCollection == null)
        {
            copyCollection.Clear();
            copyCollection = new List<int>(collection);
            return -1;
        }

        int result = copyCollection[Random.Range(0, copyCollection.Count)];
        copyCollection.Remove(result);

        return result;
    }


    private void FillingAnswers()
    {
        _poolIDsCopy = new List<int>(_poolIDsConst);
        DebugCollection(1, -1, -1, _poolIDsCopy);

        _poolIDsCopy.Remove(_answersID[0]);

        _posInPoolIDsCopy = Random.Range(0, _poolIDsCopy.Count);
        _answersID[1] = _poolIDsCopy[_posInPoolIDsCopy];
        DebugCollection(1, _posInPoolIDsCopy, _answersID[1], _poolIDsCopy);

        _poolIDsCopy.RemoveAt(_posInPoolIDsCopy);
        _posInPoolIDsCopy = Random.Range(0, _poolIDsCopy.Count);
        _answersID[2] = _poolIDsCopy[_posInPoolIDsCopy];
        DebugCollection(2, _posInPoolIDsCopy, _answersID[2], _poolIDsCopy);

        _poolIDsCopy.RemoveAt(_posInPoolIDsCopy);
        _posInPoolIDsCopy = Random.Range(0, _poolIDsCopy.Count);
        _answersID[3] = _poolIDsCopy[_posInPoolIDsCopy];
        DebugCollection(3, _posInPoolIDsCopy, _answersID[3], _poolIDsCopy);

        _poolIDsCopy.RemoveAt(_posInPoolIDsCopy);
        _posInPoolIDsCopy = Random.Range(0, _poolIDsCopy.Count);
        _answersID[4] = _poolIDsCopy[_posInPoolIDsCopy];
        DebugCollection(4, _posInPoolIDsCopy, _answersID[4], _poolIDsCopy);


        _poolIDsCopy.RemoveAt(_posInPoolIDsCopy);
        _posInPoolIDsCopy = Random.Range(0, _poolIDsCopy.Count);
        _answersID[5] = _poolIDsCopy[_posInPoolIDsCopy];
        DebugCollection(5, _posInPoolIDsCopy, _answersID[5], _poolIDsCopy);


        _poolIDsCopy.RemoveAt(_posInPoolIDsCopy);
        _posInPoolIDsCopy = Random.Range(0, _poolIDsCopy.Count);
        _answersID[6] = _poolIDsCopy[_posInPoolIDsCopy];
        DebugCollection(6, _posInPoolIDsCopy, _answersID[6], _poolIDsCopy);


        _poolIDsCopy.RemoveAt(_posInPoolIDsCopy);
        _posInPoolIDsCopy = Random.Range(0, _poolIDsCopy.Count);
        _answersID[7] = _poolIDsCopy[_posInPoolIDsCopy];
        DebugCollection(7, _posInPoolIDsCopy, _answersID[7], _poolIDsCopy);


        _poolIDsCopy.RemoveAt(_posInPoolIDsCopy);
        _posInPoolIDsCopy = Random.Range(0, _poolIDsCopy.Count);
        _answersID[8] = _poolIDsCopy[_posInPoolIDsCopy];
        DebugCollection(8, _posInPoolIDsCopy, _answersID[8], _poolIDsCopy);
    }

    private void RandomisationAnswers()
    {
        int pos = Random.Range(1, _answersID.Length);
        _answersID[pos] = _answersID[0];
    }

    private void Print() =>
        EventDoPrint?.Invoke();

    public void PrintAddictionalField() =>
        EventDoPrintAddictionalField?.Invoke();

    public void AddToPoolRightAnswers() =>
       _workWithDB.IncreaseCorrectAnswersInTableWords(_answersID[0]);

    public void SubtractToPoolRightAnswers() =>
        _workWithDB.DecreaseCorrectAnswersInTableWords(_answersID[0]);

    public int GetColor(int fieldIndex, bool isAnswerField)
    {
        int codOfColor = -1;

        if (isAnswerField)
        {
            if (_answersID[fieldIndex] == WordIdInDB)//угадали
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
            if (_isAutoMode == false)
                ProccesBody();
        }

        if (isAnswerField)
        {
            if (_answersID[fieldIndex] == WordIdInDB)//угадали
            {
                PrintAddictionalField();
                SubtractToPoolRightAnswers();

                if (_isAutoMode)
                    ProccesBody();
            }
            else
            {
                AddToPoolRightAnswers();
            }
        }
    }
}
