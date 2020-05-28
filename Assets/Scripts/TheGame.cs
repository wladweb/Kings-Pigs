using UnityEngine;
using System.Collections.Generic;

public class TheGame : MonoBehaviour
{
    [SerializeField] private Transform _roomsHolder;
    [SerializeField] private Camera _camera;

    private List<Room> _rooms = new List<Room>();
    private int _currnetRoomIndex;

    private void Awake()
    {
        for (int i = 0, l = _roomsHolder.childCount; i < l; i++)
        {
            _rooms.Add(_roomsHolder.GetChild(i).GetComponent<Room>());
        }
    }

    private void RoomChange(Room toRoom)
    { 
        //
    }
}
