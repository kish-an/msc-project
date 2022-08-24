using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortVisualiser : Displayable
{
    public int size = 10;
    public float height = 1f;
    public GameObject[] cubes;
    public float animationTime = 1f;

    private void Randomise()
    {
        cubes = new GameObject[size];

        for (int i = 0; i < size; i++)
        {
            float randNum = Random.Range(0, height);

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(this.transform);
            cube.transform.localScale = new Vector3(0.1f, randNum, 0.1f);
            cube.transform.localPosition = new Vector3(i / 5f, randNum / 2f, 0);

            cubes[i] = cube;
        }
    }

    public override void Display()
    {
        Notepad notepad = FindObjectOfType<Notepad>();
        notepad.SetHeader("Selection Sort");
        notepad.SetBody("On each iteration of selection sort, it will iterate again through the remaining items to probe for the minimum value. Once found, this will then be swapped with the current value and we can consider the element sorted. This process repeats for the remaining elements until the entire list is sorted in ascending order.\n\n<color=#292929><b>Grey</b></color> = Current element\n<color=\"blue\"><b>Blue</b></color> = Probe to find minimum\n<color=\"red\"><b>Red</b></color> = Minimum\n<color=\"green\"><b>Green</b></color> = Sorted");


        Randomise();
        SetAnimationSpeed(animationTime);
        StartCoroutine(Sort.SelectionSort(cubes));
    }

    public void SetAnimationSpeed(float speed)
    {
        Sort.animationTime = speed;
    }
}
