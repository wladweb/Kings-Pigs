using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private DoorIn _doorIn;
    [SerializeField] private DoorOut _doorOut;
    [SerializeField] private EnemiesHolder _enemies;

    private void OnEnable()
    {
        _doorIn.KingEnteredRoom += OnKingEnteredRoom;
    }

    private void OnDisable()
    {
        _doorIn.KingEnteredRoom -= OnKingEnteredRoom;
    }

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

    private void OnKingEnteredRoom()
    {
        _enemies.ChangeEnemiesState(true);
    }
}
