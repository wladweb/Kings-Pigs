using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class TheGame : MonoBehaviour
{
    [SerializeField] private Transform _roomsHolder;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private King _king;
    [SerializeField] private GameObject _winScreen; 
    [SerializeField] private GameObject _deadScreen;
    [SerializeField] private HealthBar _healthBar;
 
    private List<Room> _rooms = new List<Room>();
    private Room _currentRoom;
    private Room _previousRoom;
    private int _currentRoomIndex;

    public event UnityAction PlayerWin; 

    private void Awake()
    {
        for (int i = 0, l = _roomsHolder.childCount; i < l; i++)
        {
            _rooms.Add(_roomsHolder.GetChild(i).GetComponent<Room>());
        }
    }

    public void StartGame()
    {
        RoomChange(_currentRoomIndex);
        _healthBar.Reset();
    }

    private void OnEnable()
    {
        _king.MoveThroughExitDoor += EndLevelHandler;
        _king.KingDeadAnimationStop += OnPlayerDead;
        _cameraMover.NextRoomReached += OnNextRoomReached;
        PlayerWin += OnPlayerWin;
    }

    private void OnDisable()
    {
        _king.MoveThroughExitDoor -= EndLevelHandler;
        _king.KingDeadAnimationStop -= OnPlayerDead;
        _cameraMover.NextRoomReached -= OnNextRoomReached;
        PlayerWin -= OnPlayerWin;
    }

    private void EndLevelHandler()
    {
        _previousRoom = _currentRoom;

        if (++_currentRoomIndex >= _rooms.Count)
        {
            PlayerWin?.Invoke();
        }
        else
        {
            RoomChange(_currentRoomIndex);
        }
    }

    private void RoomChange(int roomIndex)
    {
        _currentRoom = _rooms[roomIndex];
        _cameraMover.SetBoundary(_currentRoom.GetBoundary());
        _king.transform.position = _currentRoom.GetSpawnPointPosition();

        StartCoroutine(_cameraMover.MoveCameraToNextRoom(_currentRoom));
    }

    private void OnNextRoomReached()
    {
        if (_previousRoom != null)
            _previousRoom.gameObject.SetActive(false);

        _currentRoom.StartRoom();
    }

    private void OnPlayerWin()
    {
        _winScreen.SetActive(true);
    }

    private void OnPlayerDead()
    {
        _deadScreen.SetActive(true);
        _king.gameObject.SetActive(false);
    }
}
