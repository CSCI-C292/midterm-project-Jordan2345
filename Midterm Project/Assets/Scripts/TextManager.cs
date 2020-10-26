using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    [SerializeField] RuntimeData _runtimeData;
    [SerializeField] OnScreenText[] texts;
    // Update is called once per frame
    void Update()
    {
        ShowText();
    }
    public void ShowText()
    {
        TextMeshProUGUI screenText = gameObject.GetComponent<TextMeshProUGUI>();
        screenText.text = texts[_runtimeData._currentLevel-1].text;
    }
}
