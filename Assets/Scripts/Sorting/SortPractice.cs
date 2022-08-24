using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class SortPractice : Displayable
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private TextMeshProUGUI whiteboardText;
    private int[] startIndicies = { 0, 3, 1 };
    private int[] movesPerLevel = { 1, 2, 4 };

    private Notepad notepad;

    private int _score = 0;
    private string[] _correctAnswerText =
    {
        "Incorrect, the numbers should be be in the following order:\n3   5   8   13   10   18   15   5",
        "Incorrect, the numbers should be be in the following order:\n3   7   11   12   14  23   20   16",
        "Incorrect, the numbers should be be in the following order:\n2   3    4   6   8   11   10   9",
    };

    public override void Display()
    {
        notepad = FindObjectOfType<Notepad>();
        notepad.SetHeader("Selection Sort");
        notepad.SetBody("Pick up the cubes and move them to the corresponding slot. A green outline will appear where you can place the cube.\n\n<b>Remember</b> that arrays start counting from 0, so the first cube is at index 0!");

        StartCoroutine(EnumerateLevels());
    }

    private IEnumerator EnumerateLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
             // Set whiteboard text
            whiteboardText.color = Color.black;
            whiteboardText.text = $"Starting at index {startIndicies[i]}, make the next {(movesPerLevel[i] == 1 ? "swap" : (movesPerLevel[i]) + " swaps")} of the selection sort algorithm";

            // Instantiate prefab
            var levelObj = Instantiate(levels[i], this.transform);

            levelObj.SetActive(false);
            yield return StartCoroutine(Wait(3.5f));
            levelObj.SetActive(true);

            var level = levelObj.GetComponent<IAnswerable>();
            level.MovesNeeded = (int) (movesPerLevel[i] - Math.Ceiling(i / 4.0));

            // Check answer
            yield return StartCoroutine(level.WaitForAnswer());

            if (CheckAnswer(level))
            {
                _score++;
                whiteboardText.color = Color.green;
                whiteboardText.text = "Correct!";
                yield return StartCoroutine(Wait(5f));
            }
            else
            {
                whiteboardText.color = Color.red;
                whiteboardText.text = _correctAnswerText[i];
                yield return StartCoroutine(Wait(10f));
            }

            Destroy(levelObj);
        }

        // Show score
        whiteboardText.color = Color.black;
        whiteboardText.text = $"Score: {_score}";
    }

    private bool CheckAnswer(IAnswerable level)
    {
        var objects = level.GetGameObjects();

        int[] nums = objects.Select(obj => int.Parse(obj.transform.GetComponentInChildren<TextMeshPro>().text)).ToArray();

        switch (level.Name)
        {
            case "SocketSwapOne":
                return nums.SequenceEqual(new int[] { 3, 5, 8, 13, 10, 18, 15, 5 });
            case "SocketSwapTwo":
                return nums.SequenceEqual(new int[] { 3, 7, 11, 12, 14, 23, 20, 16 });
            case "SocketSwapThree":
                return nums.SequenceEqual(new int[] { 2, 3, 4, 6, 8, 11, 10, 9 });
            default:
                return false;
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
