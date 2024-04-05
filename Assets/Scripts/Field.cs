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
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int _fieldIndex;
    [SerializeField] private bool _isQuestionField;

    private MainProcces _game;
    private TMP_Text _text;
    private Button _button;
    private Image _image;
    private Color _colorSemiRed;
    private Color _colorSemiGreen;
    private Color _colorTransend;

    private void Awake()
    {
        _game = _gameObject.GetComponent<MainProcces>();
        _text = GetComponent<TMP_Text>();
        _button = GetComponent<Button>();
        _image = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _game.EventDoResetColor += DoResetColor;
        _game.EventDoPrint += DoPrint;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _game.EventDoResetColor -= DoResetColor;
        _game.EventDoPrint -= DoPrint;
    }

    private void OnButtonClick()
    {
        if (_isQuestionField)
        {
            if (_game._isAutoMode == false)
                _game.ProccesBody();
        }
        else
        {
            if (_game._answersID[_fieldIndex] == _game._wordID)
            {
                _game.PrintingRightAnswers();

                _image.color = _colorSemiGreen;
                _game._poolRightAnswers[_game._wordID] -= 1;

                if (_game._isAutoMode == true)
                    _game.ProccesBody();
            }
            else
            {
                _image.color = _colorSemiRed;
                _game._poolRightAnswers[_game._wordID] += 1;
            }
        }
    }

    public void DoResetColor()
    {
        _image.color = _colorTransend;
    }

    public void DoPrint()
    {
        if (_isQuestionField)
            _text.text = _game._word;
        else
            _text.text = _game._answersWord[_fieldIndex];
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
