using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private float _smoothSpeed;
    [SerializeField] private King _king;
    
    private RoomBoundary _boundary;
    private bool IsRoomActive;

    public event UnityAction NextRoomReached;

    private void FixedUpdate()
    {
        if (IsRoomActive)
        {
            Vector3 needlePosition = new Vector3(
            Mathf.Clamp(_king.transform.position.x, _boundary.LeftConstraint, _boundary.RightConstraint),
            Mathf.Clamp(_king.transform.position.y, _boundary.TopConstraint, _boundary.BottomConstraint),
            transform.position.z);

            Vector3 smothedPosition = Vector3.Lerp(transform.position, needlePosition, _smoothSpeed);
            transform.position = smothedPosition;
        }
    }

    public IEnumerator MoveCameraToNextRoom(Room roomToMove)
    {
        IsRoomActive = false;

        Vector3 startLevelPosition = roomToMove.GetLevelStartPosition();
        Vector3 target = new Vector3(startLevelPosition.x, startLevelPosition.y, transform.position.z);

        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _cameraSpeed * Time.deltaTime);
            yield return null;
        }

        NextRoomReached?.Invoke();
        IsRoomActive = true;
    }

    public void SetBoundary(RoomBoundary boundary)
    {
        _boundary = boundary;
    }
}
