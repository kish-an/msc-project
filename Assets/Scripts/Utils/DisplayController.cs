using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    private static Displayable _instance;
    private Notepad notepad;

    void Start()
    {
        notepad = FindObjectOfType<Notepad>();
    }

    public void Display(Displayable prefab)
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }

        _instance = Instantiate(prefab, this.transform);
        _instance.Display();
    }

    public void Clear()
    {
        if (_instance == null) return;

        Destroy(_instance.gameObject);
        notepad.Reset();
    }

}
