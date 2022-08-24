using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sort
{
    public static float animationTime;
    private static float _easeDuration = 0.75f;

    public static IEnumerator SelectionSort(GameObject[] arr)
    {
        int min;

        for (int i = 0; i < arr.Length; i++)
        {
            min = i;

            yield return new WaitForSeconds(animationTime);
            LeanTween.color(arr[i], Color.grey, _easeDuration);

            for (int j = i + 1; j < arr.Length; j++)
            {
                yield return new WaitForSeconds(animationTime / 1.5f);
                LeanTween.color(arr[j], Color.blue, _easeDuration);

                if (arr[j].transform.localScale.y < arr[min].transform.localScale.y)
                {
                    yield return new WaitForSeconds(animationTime);

                    if (arr[min] != arr[i])
                        LeanTween.color(arr[min], Color.white, _easeDuration);

                    min = j;

                    LeanTween.color(arr[j], Color.red, _easeDuration);
                    continue;
                }

                yield return new WaitForSeconds(animationTime / 1.5f);
                LeanTween.color(arr[j], Color.white, _easeDuration);
            }


            if (min != i)
            {
                yield return new WaitForSeconds(animationTime);
                Swap(arr, i, min);
            }

            yield return new WaitForSeconds(animationTime);
            LeanTween.color(arr[min], Color.white, _easeDuration);
            LeanTween.color(arr[i], Color.green, _easeDuration);
        }
    }

    private static void Swap(GameObject[] arr, int i, int min)
    {
        // Swap GameObject array elements
        GameObject temp = arr[i];
        arr[i] = arr[min];
        arr[min] = temp;

        // Swap positions in world
        Vector3 tempPos = arr[i].transform.localPosition;

        LeanTween.moveLocalX(arr[i], arr[min].transform.localPosition.x, 1);
        LeanTween.moveLocalZ(arr[i], -0.3f, 0.5f).setLoopPingPong(1);

        LeanTween.moveLocalX(arr[min], tempPos.x, 1);
        LeanTween.moveLocalZ(arr[min], 0.3f, 0.5f).setLoopPingPong(1);
    }
}
