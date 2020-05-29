using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheGame : MonoBehaviour
{
    [SerializeField] private Transform _roomsHolder;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private King _king;
 
    private List<Room> _rooms = new List<Room>();
    private CameraMover _cameraMover;
    private Room _currentRoom;
    private Room _previousRoom;
    private int _currentRoomIndex;

    private void Awake()
    {
        for (int i = 0, l = _roomsHolder.childCount; i < l; i++)
        {
            _rooms.Add(_roomsHolder.GetChild(i).GetComponent<Room>());
        }

        _cameraMover = _camera.GetComponent<CameraMover>();

        RoomChange(_currentRoomIndex);
    }

    private void OnEnable()
    {
        _king.MoveThroughExitDoor += EndLevelHandler;
    }

    private void OnDisable()
    {
        _king.MoveThroughExitDoor -= EndLevelHandler;
    }

    private void EndLevelHandler()
    {
        _previousRoom = _currentRoom;
        _currentRoomIndex++;
        RoomChange(_currentRoomIndex);
    }

    private void RoomChange(int roomIndex)
    {
        _cameraMover.IsRoomActive = false;
        _currentRoom = _rooms[roomIndex];
        
        StartCoroutine(MoveCameraToNextRoom());
    }

    private IEnumerator MoveCameraToNextRoom()
    {
        Vector3 startLevelPosition = _currentRoom.GetLevelStartPosition();
        Vector3 target = new Vector3(startLevelPosition.x, startLevelPosition.y, _camera.transform.position.z);
        
        while (_camera.transform.position != target)
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, target, _cameraSpeed * Time.deltaTime);
            yield return null;
        }
        
        ActivateRoom();
    }

    private void ActivateRoom()
    {
        _currentRoom.StartRoom();
        _cameraMover.IsRoomActive = true;
    }
}
