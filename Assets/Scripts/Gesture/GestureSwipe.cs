using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GestureSwipe : MonoBehaviour, IAnswerable
{
    [SerializeField] protected GameObject[] objects;
    [SerializeField] protected string _levelName;
    protected int index;
    protected bool _isSwiping = false;

    private bool _answered = false;
    public string Name => _levelName;
    public bool Answered => _answered;
    public int MovesNeeded { get; set; }


    public void MoveLeft(GameObject obj)
    {
        index = Array.IndexOf(objects, obj);

        if (index == 0 || _isSwiping) return;

        Left();

        StartCoroutine(DisableSwapping(1f));
    }

    public void MoveRight(GameObject obj)
    {
        index = Array.IndexOf(objects, obj);

        if (index == objects.Length - 1 || _isSwiping) return;

        Right();

        StartCoroutine(DisableSwapping(1f));
    }

    protected abstract void Left();

    protected abstract void Right();

    public void ConfirmAnswer()
    {
        _answered = true;
    }

    private IEnumerator DisableSwapping(float seconds)
    {
        _isSwiping = true;
        yield return new WaitForSeconds(seconds);
        _isSwiping = false;
    }

    public IEnumerator WaitForAnswer()
    {
        while (!_answered)
            yield return null;
    }

    public GameObject[] GetGameObjects()
    {
        return objects;
    }
}
