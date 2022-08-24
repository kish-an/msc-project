using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnswerable
{
    string Name { get; }

    int MovesNeeded { get; set; }

    bool Answered { get; }

    IEnumerator WaitForAnswer();

    GameObject[] GetGameObjects();
}
