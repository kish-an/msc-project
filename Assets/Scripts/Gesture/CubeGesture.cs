using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGesture : GestureSwipe
{
    protected override void Left()
    {
        Swap(index, index - 1);
    }

    protected override void Right()
    {
        Swap(index, index + 1);
    }

    private void Swap(int i, int j)
    {
        GameObject temp = objects[i];
        objects[i] = objects[j];
        objects[j] = temp;

        Vector3 tempPos = objects[i].transform.localPosition;
        LeanTween.moveLocalX(objects[i], objects[j].transform.localPosition.x, 0.5f);
        LeanTween.moveLocalZ(objects[i], -0.15f, 0.5f).setLoopPingPong(1);

        LeanTween.moveLocalX(objects[j], tempPos.x, 0.5f);
        LeanTween.moveLocalZ(objects[j], 0.15f, 0.5f).setLoopPingPong(1);
    }
}
