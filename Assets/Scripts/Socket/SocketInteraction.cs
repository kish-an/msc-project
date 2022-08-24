using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

// Adapted from https://github.com/BlauBierBube/Socket_Oculus_Integration_SDK
public class SocketInteraction : MonoBehaviour
{
    [SerializeField] protected GameObject socket;
    [SerializeField] protected Material hoverMaterial;
    [SerializeField] protected bool freeze = true;
    [SerializeField] protected string interactorTag;

    public delegate void SocketEventHandler(GameObject socket, GameObject interactor);
    public event SocketEventHandler SocketEnter;
    public event SocketEventHandler SocketExit;

    protected bool _socketFree = true;
    protected Rigidbody _rigidBody;
    protected GameObject _target;
    protected GameObject _hoverObject;


    protected void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(interactorTag)) return;

        if (_socketFree)
        {
            _target = other.gameObject;
            CreateHoverObject();
        }

        // Socket is empty and hand is no longer grabbing
        if (_target.GetComponent<Grabbable>().GrabPoints.Count == 0 && _socketFree)
            PlaceAtSocket();
    }

    protected void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(interactorTag)) return;

        // Cube has been hovered over but not placed
        if (_socketFree || other.gameObject != _target)
        {
            DestroyHoverObject();
            return;
        }

        if (freeze)
            _rigidBody.constraints = RigidbodyConstraints.None;

        OnSocketExit(socket, other.gameObject);

        _socketFree = true;

    }

    protected virtual void PlaceAtSocket()
    {
        DestroyHoverObject();

        _target.transform.rotation = socket.transform.rotation;
        _target.transform.position = socket.transform.position;

        if (freeze)
        {
            _rigidBody = _target.GetComponent<Rigidbody>();
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }

        OnSocketEnter(socket, _target);

        _socketFree = false;
    }

    protected virtual void CreateHoverObject()
    {
        if (_hoverObject != null || !_socketFree) return;

        _hoverObject = Instantiate(_target, socket.transform.position, socket.transform.rotation);
        _hoverObject.transform.parent = socket.transform;

        _hoverObject.GetComponent<Collider>().enabled = false;
        _hoverObject.GetComponent<MeshRenderer>().material = hoverMaterial;
    }

    protected void DestroyHoverObject()
    {
        if (_hoverObject == null) return;

        Destroy(_hoverObject);
    }

    protected void OnSocketEnter(GameObject socket, GameObject interactor)
    {
        SocketEnter?.Invoke(socket, interactor);
    }

    protected void OnSocketExit(GameObject socket, GameObject interactor)
    {
        SocketExit?.Invoke(socket, interactor);
    }
}
