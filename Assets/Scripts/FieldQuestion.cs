using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FieldQuestion : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private Game _game;
    public TMP_Text _text;
    private Button _button;
    private Image _image;
    private Color _colorSemiRed;
    private Color _colorSemiGreen;
    private Color _colorTransend;

    private void Awake()
    {
        _game = _gameObject.GetComponent<Game>();
        _text = GetComponent<TMP_Text>();
        _button = GetComponent<Button>();
        _image = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _game.EventDoResetColor+= DoResetColor;
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
        if (_game._isAutoMode==false)
         _game.GameBody();
    }

    public void DoResetColor()
    {
        _image.color = _colorTransend;
    }

    public void DoPrint()
    {
       _text.text = _game._word;
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
