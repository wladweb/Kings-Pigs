using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private DoorIn _doorIn;

    public void StartRoom()
    {
        _doorIn.Open();
    }

    public Vector3 GetLevelStartPosition()
    {
        return _doorIn.transform.position;
    }

    public Vector3 GetSpawnPointPosition()
    {
        return _doorIn.transform.GetChild(0).position;
    }

    public RoomBoundary GetBoundary()
    {
        return transform.GetComponentInChildren<RoomBoundary>();
    }
}
