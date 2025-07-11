//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using TMPro;
//using Unity.VisualScripting;
//using System.Collections.Generic;
//using TMPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using UnityEngine.UI;
//using UnityEngine.Events;

public class ToggleDictionari : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Toggle _toggle;
    [SerializeField] private int _dictionariNumber;

    private WorkWithDB _workWithDB;
    //private DictionaryShower _dictionarisShower;
    private Text _text;

    private void Awake()
    {
        _workWithDB = GameObject.Find("WorkWithDB").GetComponent<WorkWithDB>();
    }

    private void OnEnable() =>
        _toggle.onValueChanged.AddListener(OnToggleClickkkk);

    private void OnDisable() =>
        _toggle.onValueChanged.RemoveListener(OnToggleClickkkk);

    private void OnToggleClickkkk(bool toggleState) =>
        _workWithDB.SetStateActivInTableDictionaris(toggleState, _dictionariNumber);

    private void Start()
    {
      //  _workWithDB = GameObject.Find("WorkWithDB").GetComponent<WorkWithDB>();
        // _dictionarisShower = GameObject.Find("DictionarisShower").GetComponent<DictionaryShower>();
        _text = GetComponentInChildren<Text>();


        Dictionary tempDictionary = _workWithDB.GetDictionaryFromTableDictionaris(_dictionariNumber);


        if (tempDictionary == null)
            return;

        _text.text = tempDictionary.DictName;

        _toggle.isOn = _workWithDB.ChekIfStateActivFromTableDictionaris(_dictionariNumber);

    }
}
