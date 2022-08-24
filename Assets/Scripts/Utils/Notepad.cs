using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notepad : MonoBehaviour
{
    [SerializeField] private TextMeshPro header;
    [SerializeField] private TextMeshPro body;

    private string _startHeaderText, _startBodytext;

    void Start()
    {
        _startHeaderText = header.text;
        _startBodytext = body.text;
    }

    public void SetHeader(string text)
    {
        header.text = text;
    }

    public void SetBody(string text)
    {
        body.text = text;
    }

    public void Reset()
    {
        header.text = _startHeaderText;
        body.text = _startBodytext;
    }
}
