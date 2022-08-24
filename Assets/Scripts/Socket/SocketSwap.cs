using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketSwap : MonoBehaviour, IAnswerable
{
    public GameObject[] cubes;
    public CubeSocket[] sockets;
    public static Dictionary<GameObject, Vector3> startPositions = new Dictionary<GameObject, Vector3>();

    [SerializeField] private string _levelName;
    [SerializeField] private GameObject button;
    public int MovesNeeded { get; set; }
    public string Name => _levelName;
    private bool _answered = false;
    public bool Answered => _answered;

    private int _swaps = 0;

    void Start()
    {
        if (cubes.Length != sockets.Length)
            Logger.Error("Every cube must have a corresponding socket!");

        for (int i = 0; i < cubes.Length; i++)
        {
            // Populate dictionary with start positions
            GameObject cube = cubes[i];
            startPositions[cube] = cube.transform.position;

            // Subscribe to socket event
            sockets[i].SocketEnter += Swap;
        }

        button.SetActive(false);
    }

    void Destroy()
    {
        // Unsubscribe from socket event
        foreach (CubeSocket socket in sockets)
        {
            socket.SocketEnter -= Swap;
        }
    }

    public void Swap(GameObject socket, GameObject cube)
    {
        int socketIndex = Array.IndexOf(sockets, socket.GetComponent<CubeSocket>());
        cubes[socketIndex] = cube;

        _swaps++;
        if (_swaps - cubes.Length == (MovesNeeded * 2))
        {
            button.SetActive(true);
        }
    }

    public void ConfirmAnswer()
    {
        _answered = true;
    }

    public IEnumerator WaitForAnswer()
    {
        while (!_answered)
            yield return null;
    }

    public GameObject[] GetGameObjects()
    {
        return cubes;
    }
}
