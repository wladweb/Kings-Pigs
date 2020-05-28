using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private King _king;
    [SerializeField] private float _smoothSpeed;

    [SerializeField] private RoomBoundary _boundary;

    private void FixedUpdate()
    {

        Vector3 neddlePosition = new Vector3(
            Mathf.Clamp(_king.transform.position.x, _boundary.LeftConstraint, _boundary.RightConstraint), 
            Mathf.Clamp(_king.transform.position.y, _boundary.TopConstraint, _boundary.BottomConstraint), 
            transform.position.z);

        Vector3 smothedPosition = Vector3.Lerp(transform.position, neddlePosition, _smoothSpeed);
        transform.position = smothedPosition;
    }

    public void SetBoundary(RoomBoundary boundary)
    {
        _boundary = boundary;
    }
}
