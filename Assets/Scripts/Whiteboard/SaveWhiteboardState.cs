using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveWhiteboardState : MonoBehaviour
{
    public GameObject sideMenu;
    public GameObject whiteboard;
    private static SaveWhiteboardState _instance;
    private Vector3 _originalPos;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        _originalPos = whiteboard.transform.position;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "School")
        {
            whiteboard.gameObject.SetActive(true);
            sideMenu.gameObject.SetActive(false);
            whiteboard.transform.position = new Vector3(0.136927605f,1.62777343f,3.79399991f);
        }
        else if (scene.name == "Whiteboard")
        {
            whiteboard.gameObject.SetActive(true);
            sideMenu.gameObject.SetActive(true);
            whiteboard.transform.position = _originalPos;
        }
        else
        {
          whiteboard.SetActive(false);
          sideMenu.SetActive(false);
        }
    }


}
