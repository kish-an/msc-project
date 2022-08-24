using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInteractables : MonoBehaviour
{
    private struct Origin
    {
        public Vector3 startPos;
        public Quaternion startRotation;

        public Origin(Vector3 startPos, Quaternion startRotation)
        {
            this.startPos = startPos;
            this.startRotation = startRotation;
        }
    }

    [SerializeField] private GameObject[] interactables;
    private Dictionary<GameObject, Origin> _originMap = new Dictionary<GameObject, Origin>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject interactable in interactables)
        {
            _originMap[interactable] = new Origin(interactable.transform.position, interactable.transform.rotation);
        }
    }

    public void ReturnToOrigin()
    {
        foreach(KeyValuePair<GameObject, Origin> interactable in _originMap)
        {
            interactable.Key.transform.position = interactable.Value.startPos;
            interactable.Key.transform.rotation = interactable.Value.startRotation;
        }
    }
}
