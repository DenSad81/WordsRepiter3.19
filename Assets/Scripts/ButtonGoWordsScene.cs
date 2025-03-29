//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonGoWordsScene : MonoBehaviour
{
    private Button _button;

    private void Awake() =>
        _button = GetComponent<Button>();

    private void OnEnable() =>
        _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick() =>
        SceneManager.LoadScene("WordsScene");
}
