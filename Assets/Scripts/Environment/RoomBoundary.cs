using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomBoundary : MonoBehaviour
{
    private float _colliderHalfWidth;
    private float _colliderHalfHeight;
    private float _cameraHalfWidth;
    private float _cameraHalfHeight;
    private float _leftConstraint;
    private float _rightConstraint;
    private float _topConstraint;
    private float _bottomtConstraint;

    public float LeftConstraint => _leftConstraint;
    public float RightConstraint => _rightConstraint;
    public float TopConstraint => _topConstraint;
    public float BottomConstraint => _bottomtConstraint;

    private void Start()
    {
        _colliderHalfWidth = GetComponent<BoxCollider2D>().size.x / 2;
        _colliderHalfHeight = GetComponent<BoxCollider2D>().size.y / 2;
        _cameraHalfHeight = Camera.main.orthographicSize;
        _cameraHalfWidth = _cameraHalfHeight * Camera.main.aspect;

        CalculateBoundaries();
    }
     
    private void CalculateBoundaries()
    {
        _leftConstraint = transform.position.x - _colliderHalfWidth + _cameraHalfWidth;
        _rightConstraint = transform.position.x + _colliderHalfWidth - _cameraHalfWidth;
        
        _topConstraint = transform.position.y - _colliderHalfHeight + _cameraHalfHeight;
        _bottomtConstraint = transform.position.y + _colliderHalfHeight - _cameraHalfHeight;
    }
}
