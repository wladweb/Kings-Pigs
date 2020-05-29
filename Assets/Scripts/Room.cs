using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private DoorIn _doorIn;
    [SerializeField] private DoorOut _doorOut;

    public void StartRoom()
    {
        _doorIn.Open();
    }

    public Vector3 GetLevelStartPosition()
    {
        return _doorIn.transform.position;
    }
}
