using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSocket : SocketInteraction
{
    protected override void PlaceAtSocket()
    {
        DestroyHoverObject();

        // Place at sockets x position but targets y and z position (keeps cubes at the same height)
        Vector3 startPos = SocketSwap.startPositions[_target];
        _target.transform.rotation = socket.transform.rotation;
        _target.transform.position = new Vector3(socket.transform.position.x, startPos.y, startPos.z);

        if (freeze)
        {
            _rigidBody = _target.GetComponent<Rigidbody>();
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }

        _socketFree = false;
        OnSocketEnter(socket, _target);
    }

    protected override void CreateHoverObject()
    {
        if (_hoverObject != null || !_socketFree) return;

        // Make hover object match cube height
        Vector3 startPos = SocketSwap.startPositions[_target];
        Vector3 newPos = new Vector3(socket.transform.position.x, startPos.y, startPos.z);

        _hoverObject = Instantiate(_target, newPos, socket.transform.rotation);
        _hoverObject.transform.parent = socket.transform;

        _hoverObject.GetComponent<Collider>().enabled = false;
        _hoverObject.GetComponent<MeshRenderer>().material = hoverMaterial;
    }
}
